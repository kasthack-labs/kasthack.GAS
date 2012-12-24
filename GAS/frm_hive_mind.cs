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
    public partial class frm_hive_mind : Form
    {
        
        public frm_hive_mind()
        {
            InitializeComponent();
            this.Width = 0;
            this.Height = 0;
        //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void frm_hive_mind_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rsz(4);

            if (this.Width >= 325 && this.Height >= 346)
            { 
                timer1.Stop();
            }
        }
        void rsz(int diff)
        {
          
            this.Width += diff;
            this.Height += diff;
            this.Location= new Point(this.Location.X- diff / 2,this.Location.Y - diff / 2);
        }

        private void frm_hive_mind_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(60);
                this.Opacity = this.Opacity - 0.1;
            }
        }
    }
}

