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
        private string nama, alamat, no_ponsel, jenis_kamar;
        private int id,jumkamar, jumhari;
        static string connectionInfo = "datasource=localhost;port=3306;username=root;password=katasandi;database=coba;SslMode=none";
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
            id = Convert.ToInt32(tbNomorIdentitasK.Text);
            nama = tbNamaK.Text;
            alamat = tbAlamatK.Text;
            no_ponsel = tbNomorPonselK.Text;
            jumkamar = Convert.ToInt32(nudJumlahKamarK.Value);
            jumhari = Convert.ToInt32(nudJumlahHariK.Value);
            Confirm.Show();
            if(Confirm.Konfirmasi)
            {
                Confirm.Hide();
                insertData(id,nama,alamat,no_ponsel,jumkamar,jumhari,jenis_kamar);
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
        private void insertData(int id, string nama, string alamat, string no_ponsel, int jum_kamar
            , int jum_hari, string jenis_kamar)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO tamu_kamar VALUES(@id_tamuk,@nama,@alamat,@nomor_ponsel,@jumlah_kamar,@jumlah_hari,@jenis_kamar,@status)", connect);
                command.Parameters.AddWithValue("@id_tamuk", id);
                command.Parameters.AddWithValue("@nama", nama);
                command.Parameters.AddWithValue("@alamat", alamat);
                command.Parameters.AddWithValue("@nomor_ponsel", no_ponsel);
                command.Parameters.AddWithValue("@jumlah_kamar", jum_kamar);
                command.Parameters.AddWithValue("@jumlah_hari", jum_hari);
                command.Parameters.AddWithValue("@jenis_kamar",jenis_kamar);
                command.Parameters.AddWithValue("@status", "Checked In");
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
            string query = "SELECT * FROM user"; //TODO : SELECT query must be changed
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
    }
}
