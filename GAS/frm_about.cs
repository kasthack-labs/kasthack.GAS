using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GAS
{
    public partial class frm_about : Form
    {
        public frm_about()
        {
            InitializeComponent();

            this.Width = 218;
            this.Height = 0;
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (this.Height < 519)
            {
                rsz(8);
                return;
            }
            //&& this.Width >=218
            //  )    
            if (this.Width < 600)
            {
                this.Width += 8;
                return; 
            }
            timer1.Stop();
            MessageBox.Show("");
        }

        private void frm_about_Load(object sender, EventArgs e)
        {
            
        }
        void rsz(int diff)
        {
            // this.Width += diff;
            this.Height += diff;
            this.Location = new Point(this.Location.X - diff / 2,
                this.Location.Y - diff / 2);
        }

        private void frm_about_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(60);
                this.Opacity = this.Opacity - 0.1;
            }

            
        }
    }
}
