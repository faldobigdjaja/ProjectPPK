﻿using System;
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
    public partial class formKonfirmasiS : Form
    {
        private bool disimpan;
        public Boolean Konfirmasi
        {
            get
            {
                return disimpan;
            }
        }
        public formKonfirmasiS()
        {
            InitializeComponent();
        }

        private void btnOke_Click(object sender, EventArgs e)
        {
            disimpan = true;
        }
    }
}
