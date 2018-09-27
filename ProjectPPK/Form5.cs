using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPPK
{
    public partial class formKonfirmasi : Form
    {
        private string disimpan = "";
        public String Konfirmasi
        {
            get
            {
                return disimpan;
            }
        }
        public formKonfirmasi()
        {
            InitializeComponent();
        }

        private void btnYa_Click(object sender, EventArgs e)
        {
            disimpan = "ya";
        }

        private void btnTidak_Click(object sender, EventArgs e)
        {
            disimpan = "tidak";
        }
    }
}
