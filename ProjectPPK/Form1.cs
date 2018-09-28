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
        private string id,nama, alamat, no_telp, jenis_kamar,jenis_paket;
        private int jumkamar, jumhari,harga_kamar,jumorang,harga_restoran;
        private RichTextBox invoice;
        static string connectionInfo = "datasource=localhost;port=3306;username=root;password=katasandi;database=hotel;SslMode=none";
        MySqlConnection connect = new MySqlConnection(connectionInfo);
        public formReservasi()
        {
            InitializeComponent();
            printDocument1.BeginPrint += beginPrint;
            printDocument1.PrintPage += printPage;
            printDocument2.BeginPrint += beginPrint;
            printDocument2.PrintPage += printPage;
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
        private void loadData_Restaurant()
        {
            string query = "SELECT * FROM reservasi_restoran";
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
        private void loadData_Rooms()
        {
            string query = "SELECT * FROM reservasi_ruangan";
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
        private void loadData_Taxi()
        {
            string query = "SELECT * FROM reservasi_taksi";
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
        private void loadData_Laundry()
        {
            string query = "SELECT * FROM laundry";
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

        private void rbStandardR_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Text)
            {
                case "Standard":
                    jenis_paket = "Standard";
                    break;
                case "Deluxe":
                    jenis_paket = "Deluxe";
                    break;
                case "Suite":
                    jenis_paket = "Suite";
                    break;
                default:
                    break;
            }
        }

        private void bTampildataR_Click(object sender, EventArgs e)
        {
            loadData_Restaurant();
        }

        private void bHapusdataR_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasR.Text;
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM reservasi_restoran WHERE no_id = @id", connect);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus", "Menghapus data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connect.Close();
                loadData_Restaurant();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btTampilDataRu_Click(object sender, EventArgs e)
        {
            loadData_Rooms();
        }

        private void btnTampilDataT_Click(object sender, EventArgs e)
        {
            loadData_Taxi();
        }

        private void btnTampilDataL_Click(object sender, EventArgs e)
        {
            loadData_Laundry();
        }

        private void btnPrintR_Click(object sender, EventArgs e)
        {
            invoice = new RichTextBox();
            invoice.SelectAll();
            invoice.SelectionFont = new Font("Verdana", 18, FontStyle.Regular);
            invoice.Text += "Hotel Inn" + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "ID Pemesan     : " + id + "\n";
            invoice.Text += "Nama           : " + nama + "\n";
            invoice.Text += "Alamat         : " + alamat + "\n";
            invoice.Text += "Nomor telpon   : " + no_telp + "\n";
            invoice.Text += "Jumlah orang   : " + jumorang + " orang" + "\n";
            invoice.Text += "Jumlah hari    : " + jumhari + " hari" + "\n";
            invoice.Text += "Jenis paket    : " + jenis_paket + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "Harga reservasi    : Rp " + harga_restoran + "\n";
            if (printDialog2.ShowDialog() == DialogResult.OK)
            {
                printDocument2.Print();
            }
        }

        private void btnSaveR_Click(object sender, EventArgs e)
        {
            nama = tbNamaR.Text;
            id = tbNomorIdentitasR.Text;
            alamat = tbAlamatR.Text;
            no_telp = tbNomorPonselR.Text;
            jumorang = Convert.ToInt32(nudJumlahOrangR.Value);
            jumhari = Convert.ToInt32(nudJumlahHariR.Value);
            insertData_Resto(id, nama, alamat, no_telp, jumorang, jumhari, jenis_paket);
            lblHargaR.Text = "" + harga_restoran;
            //melakukan reset / mengosongkan form
            tbNamaR.Text = "";
            tbAlamatR.Text = "";
            tbNomorPonselR.Text = "";
            nudJumlahOrangR.Value = 0;
            nudJumlahHariR.Value = 0;
        }

        private void btnCheckOutK_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM reservasi_kamar WHERE no_id = @id", connect);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus","Menghapus data",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                string nama_table = Convert.ToString(selectedRow.Cells["nama"].Value);
                string id_table = Convert.ToString(selectedRow.Cells["no_id"].Value);
                string alamat_table = Convert.ToString(selectedRow.Cells["alamat"].Value);
                string notelp_table = Convert.ToString(selectedRow.Cells["no_telp"].Value);
                string jumkamar_table = Convert.ToString(selectedRow.Cells["jumlah_kamar"].Value);
                string jumhari_table = Convert.ToString(selectedRow.Cells["jumlah_hari"].Value);
                string jeniskamar_table = Convert.ToString(selectedRow.Cells["jenis_kamar"].Value);
                string hargakamar_table = Convert.ToString(selectedRow.Cells["harga_kamar"].Value);
                

                nama = nama_table;
                id = id_table;
                alamat = alamat_table;
                no_telp = notelp_table;
                jumkamar = Convert.ToInt32(jumkamar_table);
                jumhari = Convert.ToInt32(jumhari_table);
                jenis_kamar = jeniskamar_table;
                harga_kamar = Convert.ToInt32(hargakamar_table);

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
        private void insertData_Resto(string id, string nama, string alamat, string no_ponsel, int jum_orang
            , int jum_hari, string jenis_paket)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO reservasi_restoran VALUES(@nama,@id,@alamat,@nomor_ponsel,@jumlah_orang,@jumlah_hari,@jenis_paket,@harga_restoran)", connect);
                harga_restoran = hitungRestoran(jum_orang, jum_hari, jenis_paket);
                command.Parameters.AddWithValue("@nama", nama);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@alamat", alamat);
                command.Parameters.AddWithValue("@nomor_ponsel", no_ponsel);
                command.Parameters.AddWithValue("@jumlah_orang", jum_orang);
                command.Parameters.AddWithValue("@jumlah_hari", jum_hari);
                command.Parameters.AddWithValue("@jenis_paket", jenis_paket);
                command.Parameters.AddWithValue("@harga_restoran", harga_restoran);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Menyimpan data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private int hitungRestoran(int jumkmr, int jumhari, string jenis)
        {
            int harga = 0;

            switch (jenis)
            {
                case "Standard":
                    harga = (jumkmr * 150000) * jumhari;
                    break;
                case "Deluxe":
                    harga = (jumkmr * 300000) * jumhari;
                    break;
                case "Suite":
                    harga = (jumkmr * 1000000) * jumhari;
                    break;
            }
            return harga;
        }
    }
}
