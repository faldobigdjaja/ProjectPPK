using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ProjectPPK
{
    public partial class formReservasi : Form
    {
        private formKonfirmasi Confirm;
        private formKonfirmasiB FormBenar;
        private formKonfirmasiS FormSalah;
        private formKesalahan FormError;
        MySqlDataAdapter mySqlDataAdapter;
        private string id,nama, alamat, no_telp, jenis_kamar;
        private int jumkamar, jumhari,harga_kamar;
        static string connectionInfo = "datasource=localhost;port=3306;username=root;password=katasandi;database=hotel;SslMode=none";
        MySqlConnection connect = new MySqlConnection(connectionInfo);

        

        public formReservasi()
        {
            InitializeComponent();
            Confirm = new formKonfirmasi();
            FormBenar = new formKonfirmasiB();
            FormSalah = new formKonfirmasiS();
        }
        private void btnSaveK_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasK.Text;
            nama = tbNamaK.Text;
            alamat = tbAlamatK.Text;
            no_telp = tbNomorPonselK.Text;
            jumkamar = Convert.ToInt32(nudJumlahKamarK.Value);
            jumhari = Convert.ToInt32(nudJumlahHariK.Value);
            Confirm.Show();
            if(Confirm.Konfirmasi)
            {
                Confirm.Hide();
                insertData(id,nama,alamat,no_telp,jumkamar,jumhari,jenis_kamar);
                FormBenar.Show();
                if(FormBenar.Konfirmasi)
                {
                    FormBenar.Hide();
                }
            } else
            {
                Confirm.Hide();
                FormSalah.Show();
                if(FormSalah.Konfirmasi)
                {
                    FormSalah.Hide();
                }
            }
        }
        private void rbStandardK_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch(rb.Text)
            {
                case "Standard":
                    jenis_kamar = "Standard";
                    break;
                case "Deluxe":
                    jenis_kamar = "Deluxe";
                    break;
                case "Suite":
                    jenis_kamar = "Suite";
                    break;
                default:
                    break;
            }
        }
        private void insertData(string id, string nama, string alamat, string no_ponsel, int jum_kamar
            , int jum_hari, string jenis_kamar)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO reservasi_kamar VALUES(@nama,@id,@alamat,@nomor_ponsel,@jumlah_kamar,@jumlah_hari,@jenis_kamar,@harga_kamar)", connect);
                int hasil = hitungKamar(jum_kamar, jum_hari, jenis_kamar);
                command.Parameters.AddWithValue("@nama", nama);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@alamat", alamat);
                command.Parameters.AddWithValue("@nomor_ponsel", no_ponsel);
                command.Parameters.AddWithValue("@jumlah_kamar", jum_kamar);
                command.Parameters.AddWithValue("@jumlah_hari", jum_hari);
                command.Parameters.AddWithValue("@jenis_kamar",jenis_kamar);
                command.Parameters.AddWithValue("@harga_kamar", hasil);
                command.ExecuteNonQuery();
                connect.Close();
            } catch(Exception ex)
            {
                FormError = new formKesalahan(ex.Message);
                FormError.Show();
                if(FormError.Konfirmasi)
                {
                    FormError.Hide();
                }
            }
        }
        private void loadData_Room()
        {
            string query = "SELECT * FROM reservasi_kamar"; //TODO : SELECT query must be changed
            try
            {
                connect.Open();
                mySqlDataAdapter = new MySqlDataAdapter(query, connectionInfo);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connect.Close();
            }
            catch (Exception ex)
            {
                FormError = new formKesalahan(ex.Message);
                FormError.Show();
                if (FormError.Konfirmasi)
                {
                    FormError.Hide();
                }
            }
        }
        private void formReservasi_Load(object sender, EventArgs e)
        {
            loadData_Room();
        }
        private int hitungKamar(int jumkmr, int jumhari, string jenis)
        {
            int harga = 0;
                
            switch(jenis)
            {
                case "Standard":
                    harga = (jumkmr * 750000) * jumhari;
                    break;
                case "Deluxe":
                    harga = (jumkmr * 1500000) * jumhari;
                    break;
                case "Suite":
                    harga = (jumkmr * 3000000) * jumhari;
                    break;
            }
            return harga;
        }
    }
}
