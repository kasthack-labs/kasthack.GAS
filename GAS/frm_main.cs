using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GAS
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {

        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            frm_about frmabout = new frm_about();
            frmabout.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_hive_mind frmhm = new frm_hive_mind();
            frmhm.ShowDialog();
        }
    }
}
