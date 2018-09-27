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
        MySqlDataAdapter mySqlDataAdapter;
        private string id,nama, alamat, no_telp, jenis_kamar;
        private int jumkamar, jumhari,harga_kamar;
        private RichTextBox invoice;
        static string connectionInfo = "datasource=localhost;port=3306;username=root;password=katasandi;database=hotel;SslMode=none";
        MySqlConnection connect = new MySqlConnection(connectionInfo);
        public formReservasi()
        {
            InitializeComponent();
            printDocument1.BeginPrint += beginPrint;
            printDocument1.PrintPage += printPage;
        }
        private void formReservasi_Load(object sender, EventArgs e)
        {
            loadData_Room();
        }
        private void loadData_Room()
        {
            string query = "SELECT * FROM reservasi_kamar";
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
                MessageBox.Show(ex.Message);
            }
        }
        private int charFrom;
        private void beginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            charFrom = 0;
        }
        private void printPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.HasMorePages = RichTextBoxPrinter.Print(invoice, ref charFrom, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadData_Room();
        }

        private void btnPrintK_Click(object sender, EventArgs e)
        {
            invoice = new RichTextBox();
            invoice.SelectAll();
            invoice.SelectionFont = new Font("Verdana",18, FontStyle.Regular);
            invoice.Text += "Hotel Inn" + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "ID Tamu        : " + id + "\n";
            invoice.Text += "Nama           : " + nama + "\n";
            invoice.Text += "Alamat         : " + alamat + "\n";
            invoice.Text += "Nomor telpon   : " + no_telp + "\n";
            invoice.Text += "Jumlah kamar   : " + jumkamar + " kamar" + "\n";
            invoice.Text += "Jumlah hari    : " + jumhari + " hari" + "\n";
            invoice.Text += "Jenis kamar    : " + jenis_kamar + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "Harga kamar    : Rp " + harga_kamar + "\n";
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void btnCheckOutK_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM reservasi_kamar WHERE no_id = @id", connect);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus, ID : " + id,"Menghapus data",MessageBoxButtons.OK,MessageBoxIcon.Information);
                connect.Close();
                loadData_Room();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string id_table = Convert.ToString(selectedRow.Cells["no_id"].Value);

                id = id_table;

            }
        }

        private void btnSaveK_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasK.Text;
            nama = tbNamaK.Text;
            alamat = tbAlamatK.Text;
            no_telp = tbNomorPonselK.Text;
            jumkamar = Convert.ToInt32(nudJumlahKamarK.Value);
            jumhari = Convert.ToInt32(nudJumlahHariK.Value);
            insertData(id,nama,alamat,no_telp,jumkamar,jumhari,jenis_kamar);
            lblHargaK.Text = "" + harga_kamar;
            //melakukan reset / mengosongkan form
            tbNomorIdentitasK.Text = "";
            tbNamaK.Text = "";
            tbAlamatK.Text = "";
            tbNomorPonselK.Text = "";
            nudJumlahKamarK.Value = 0;
            nudJumlahHariK.Value = 0;
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
                harga_kamar = hitungKamar(jum_kamar, jum_hari, jenis_kamar);
                command.Parameters.AddWithValue("@nama", nama);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@alamat", alamat);
                command.Parameters.AddWithValue("@nomor_ponsel", no_ponsel);
                command.Parameters.AddWithValue("@jumlah_kamar", jum_kamar);
                command.Parameters.AddWithValue("@jumlah_hari", jum_hari);
                command.Parameters.AddWithValue("@jenis_kamar",jenis_kamar);
                command.Parameters.AddWithValue("@harga_kamar", harga_kamar);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Menyimpan data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Terjadi kesalahan",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
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
