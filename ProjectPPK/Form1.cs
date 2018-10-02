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
        private string id,nama, alamat, no_telp, jenis_kamar,jenis_paket,keperluan,alamat_tujuan;
        private int jumkamar, jumhari,harga_kamar,jumorang,harga_restoran,jumlah_laundry,harga_ruangan,harga_taxi,harga_laundry;
        private RichTextBox invoice;
        //konfigurasi database harap dikembalikan ke semula jika setelah diubah dan telah digunakan untuk ujicoba
        static string connectionInfo = "datasource=localhost;port=3306;username=root;password=katasandi;database=hotel;SslMode=none";
        MySqlConnection connect = new MySqlConnection(connectionInfo);
        public formReservasi()
        {
            InitializeComponent();
            printDocument1.BeginPrint += beginPrint;
            printDocument1.PrintPage += printPage;

            printDocument2.BeginPrint += beginPrint;
            printDocument2.PrintPage += printPage;

            printDocument3.BeginPrint += beginPrint;
            printDocument3.PrintPage += printPage;

            printDocument4.BeginPrint += beginPrint;
            printDocument4.PrintPage += printPage;

            printDocument5.BeginPrint += beginPrint;
            printDocument5.PrintPage += printPage;
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
        //Tombol tampil data digunakan untuk menampilkan data dari tabel reservasi_kamar
        private void button1_Click(object sender, EventArgs e)
        {
            loadData_Room();
        }
        //Tombol print berfungsi untuk mencetak dokumen invoice
        private void btnPrintK_Click(object sender, EventArgs e)
        {
            invoice = new RichTextBox();
            invoice.SelectAll();
            invoice.SelectionFont = new Font("Verdana",18, FontStyle.Regular);
            invoice.Text += "Hotel Inn" + "\n";
            invoice.Text += "Invoice reservasi kamar " + "\n";
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
        //Tombol tampil data digunakan untuk menampilkan data dari tabel reservasi_restoran
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
        //Tombol tampil data digunakan untuk menampilkan data dari tabel reservasi_ruangan
        private void btTampilDataRu_Click(object sender, EventArgs e)
        {
            loadData_Rooms();
        }
        //Tombol hapus data untuk menghapus data
        private void btHapusDataRu_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasRu.Text;
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM reservasi_ruangan WHERE no_id = @id", connect);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus", "Menghapus data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connect.Close();
                loadData_Rooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //tombol hapus data untuk menghapus data
        private void btnHapusDataT_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasT.Text;
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM reservasi_taksi WHERE no_id = @id", connect);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus", "Menghapus data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connect.Close();
                loadData_Taxi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Tombol hapus data untuk menghapus data
        private void btnHapusDataL_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasL.Text;
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM laundry WHERE no_id = @id", connect);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus", "Menghapus data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connect.Close();
                loadData_Laundry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /**
         * Tombol simpan berfungsi untuk menyimpan data hasil inputan pengguna
         * ke dalam database dengan menggunakan method insertData(). method ini
         * berisi query untuk memasukkan data.
         * **/
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasT.Text;
            alamat_tujuan = tbAlamatTujuanT.Text;
            no_telp = tbNomorPonselT.Text;
            jumorang = Convert.ToInt32(nudJumlahOrangT.Value);
            insertData_Taksi(id, alamat_tujuan, no_telp, jumorang);
            lblHargaT.Text = " " + harga_taxi;
            //melakukan reset
            tbAlamatTujuanT.Text = "";
            tbNomorPonselT.Text = "";
            nudJumlahOrangT.Value = 0;
        }
        /**
         * Tombol simpan berfungsi untuk menyimpan data hasil inputan pengguna
         * ke dalam database dengan menggunakan method insertData(). method ini
         * berisi query untuk memasukkan data.
         * **/
        private void btnSimpanL_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasL.Text;
            jumlah_laundry = Convert.ToInt32(nudJumlahkgL.Value);
            insertData_Laundry(id, jumlah_laundry);
            lblHargaL.Text = "" + harga_laundry;
            //melakukan reset / mengosongkan form
            nudJumlahkgL.Value = 0;
        }
        //Tombol tampil data digunakan untuk menampilkan data dari tabel reservasi_taksi
        private void btnTampilDataT_Click(object sender, EventArgs e)
        {
            loadData_Taxi();
        }
        /**
         * Tombol simpan berfungsi untuk menyimpan data hasil inputan pengguna
         * ke dalam database dengan menggunakan method insertData(). method ini
         * berisi query untuk memasukkan data.
         * **/
        private void btnSimpanRu_Click(object sender, EventArgs e)
        {
            nama = tbNamaRu.Text;
            id = tbNomorIdentitasRu.Text;
            alamat = tbAlamatRu.Text;
            no_telp = tbNomorPonselRu.Text;
            jumorang = Convert.ToInt32(nudJumlahOrangRu.Value);
            jumhari = Convert.ToInt32(nudJumlahHariRu.Value);
            keperluan = tbKeperluanRu.Text;
            insertData_Ruangan(id, nama, alamat, no_telp, jumorang, jumhari, keperluan);
            //mengosongkan form
            lblHargaRu.Text = Convert.ToString(harga_ruangan);
            tbNamaRu.Text = "";
            tbNomorPonselRu.Text = "";
            tbAlamatRu.Text = "";
            nudJumlahOrangRu.Value = 0;
            nudJumlahHariRu.Value = 0;
            tbKeperluanRu.Text = "";
        }
        //Tombol tampil data digunakan untuk menampilkan data dari tabel laundry
        private void btnTampilDataL_Click(object sender, EventArgs e)
        {
            loadData_Laundry();
        }
        //tombol print untuk mencetak invoice reservasi restoran
        private void btnPrintR_Click(object sender, EventArgs e)
        {
            invoice = new RichTextBox();
            invoice.SelectAll();
            invoice.SelectionFont = new Font("Verdana", 18, FontStyle.Regular);
            invoice.Text += "Hotel Inn" + "\n";
            invoice.Text += "Invoice reservasi restoran " + "\n";
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
        //tombol print untuk mencetak invoice reservasi ruangan
        private void btnPrintRu_Click(object sender, EventArgs e)
        {
            invoice = new RichTextBox();
            invoice.SelectAll();
            invoice.SelectionFont = new Font("Verdana", 18, FontStyle.Regular);
            invoice.Text += "Hotel Inn" + "\n";
            invoice.Text += "Invoice reservasi ruangan " + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "ID Pemesan     : " + id + "\n";
            invoice.Text += "Nama           : " + nama + "\n";
            invoice.Text += "Alamat         : " + alamat + "\n";
            invoice.Text += "Nomor telpon   : " + no_telp + "\n";
            invoice.Text += "Jumlah orang   : " + jumorang + " orang" + "\n";
            invoice.Text += "Jumlah hari    : " + jumhari + " hari" + "\n";
            invoice.Text += "Keperluan      : " + keperluan + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "Harga reservasi    : Rp " + harga_ruangan + "\n";
            if (printDialog3.ShowDialog() == DialogResult.OK)
            {
                printDocument3.Print();
            }
        }
        //tombol print untuk mencetak invoice reservasi taksi
        private void btnPrintT_Click(object sender, EventArgs e)
        {
            invoice = new RichTextBox();
            invoice.SelectAll();
            invoice.SelectionFont = new Font("Verdana", 18, FontStyle.Regular);
            invoice.Text += "Hotel Inn" + "\n";
            invoice.Text += "Invoice reservasi taksi " + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "ID Pemesan     : " + id + "\n";
            invoice.Text += "Alamat tujuan  : " + alamat_tujuan + "\n";
            invoice.Text += "Nomor telpon   : " + no_telp + "\n";
            invoice.Text += "Jumlah orang   : " + jumorang + " orang" + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "Harga reservasi    : Rp " + harga_taxi + "\n";
            if (printDialog4.ShowDialog() == DialogResult.OK)
            {
                printDocument4.Print();
            }
        }
        //tombol print untuk mencetak invoice laundry hotel
        private void btnPrintL_Click(object sender, EventArgs e)
        {
            invoice = new RichTextBox();
            invoice.SelectAll();
            invoice.SelectionFont = new Font("Verdana", 18, FontStyle.Regular);
            invoice.Text += "Hotel Inn" + "\n";
            invoice.Text += "Invoice laundry hotel " + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "ID Pemesan             : " + id + "\n";
            invoice.Text += "Jumlah laundry (kg)    : " + jumlah_laundry + " kg" + "\n";
            invoice.Text += "=========================================================" + "\n";
            invoice.Text += "Harga laundry    : Rp " + harga_laundry + "\n";
            if (printDialog5.ShowDialog() == DialogResult.OK)
            {
                printDocument5.Print();
            }
        }

        /**
* Tombol simpan berfungsi untuk menyimpan data hasil inputan pengguna
* ke dalam database dengan menggunakan method insertData(). method ini
* berisi query untuk memasukkan data.
* **/
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
        //tombol Check Out digunakan untuk menghapus data pada tabel reservasi_kamar
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
        /**
         * Tombol simpan berfungsi untuk menyimpan data hasil inputan pengguna
         * ke dalam database dengan menggunakan method insertData(). method ini
         * berisi query untuk memasukkan data.
         * **/
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
                connect.Close();
            }
        }
        private void insertData_Taksi(string id,string alamat_tjn, string no_ponsel, int jum_org)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO reservasi_taksi VALUES(@id,@alamat_tujuan,@nomor_ponsel,@jumlah_orang,@harga_taksi)", connect);
                harga_taxi = hitungTaksi(jum_org);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@alamat_tujuan", alamat_tjn);
                command.Parameters.AddWithValue("@nomor_ponsel", no_ponsel);
                command.Parameters.AddWithValue("@jumlah_orang", jum_org);
                command.Parameters.AddWithValue("@harga_taksi", harga_taxi);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Menyimpan data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                connect.Close();
            }
        }
        private void insertData_Ruangan(string id, string nama, string alamat, string no_ponsel, int jum_orang
            , int jum_hari, string keperluan)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO reservasi_ruangan VALUES(@nama,@id,@alamat,@nomor_ponsel,@jumlah_orang,@jumlah_hari,@keperluan,@harga)", connect);
                harga_ruangan = hitungRuangan(jum_orang, jum_hari);
                command.Parameters.AddWithValue("@nama", nama);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@alamat", alamat);
                command.Parameters.AddWithValue("@nomor_ponsel", no_ponsel);
                command.Parameters.AddWithValue("@jumlah_orang", jum_orang);
                command.Parameters.AddWithValue("@jumlah_hari", jum_hari);
                command.Parameters.AddWithValue("@keperluan", keperluan);
                command.Parameters.AddWithValue("@harga", harga_ruangan);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Menyimpan data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void insertData_Laundry(string id, int jum_kg)
        {
            try
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO laundry VALUES(@id,@jumlah_kg,@harga)", connect);
                harga_laundry = hitungLaundry(jum_kg);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@jumlah_kg", jum_kg);
                command.Parameters.AddWithValue("@harga", harga_laundry);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Menyimpan data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Terjadi kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connect.Close();
            }
        }
        //fungsi untuk menghitung harga kamar
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
        //fungsi untuk menghitung harga laundry 
        private int hitungLaundry(int jumkg)
        {
            return jumkg * 25000;
        }
        //fungsi untuk menghitung harga biaya taksi
        private int hitungTaksi(int jum_org)
        {
            return jum_org * 12000;
        }
        //fungsi untuk menghitung harga ruangan
        private int hitungRuangan(int jum_org, int jum_hari)
        {
            return (jum_org * 25000) * jum_hari;
        }
        //fungsi untuk menghitung harga restoran
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
