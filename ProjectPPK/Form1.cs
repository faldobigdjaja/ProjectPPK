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

        private string nama, id, alamat, no_ponsel, jenis_kamar;
        private int jumkamar, jumhari;
        static string connectionInfo = "datasource=localhost;port=3306;username=root;password=katasandi;database=coba;SslMode=none";
        MySqlConnection connect = new MySqlConnection(connectionInfo);
        public formReservasi()
        {
            InitializeComponent();
            Confirm = new formKonfirmasi();
            FormBenar = new formKonfirmasiB();
            FormSalah = new formKonfirmasiS();
            FormError = new formKesalahan();
        }
        private void btnSaveK_Click(object sender, EventArgs e)
        {
            id = tbNomorIdentitasK.Text;
            nama = tbNamaK.Text;
            alamat = tbAlamatK.Text;
            no_ponsel = tbNomorPonselK.Text;
            jumkamar = Convert.ToInt32(nudJumlahKamarK.Value);
            jumhari = Convert.ToInt32(nudJumlahHariK.Value);
            Confirm.Show();
            if(Confirm.Konfirmasi)
            {
                Confirm.Hide();
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
        private void insertData()
        {

        }
    }
}
