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
           // this.Width = 0;
           // this.Height = 0;
           
        }

        private void frm_hive_mind_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(60);
                this.Opacity = this.Opacity + 0.1;
            }
        }
 /*

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
*/
        private void frm_hive_mind_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(60);
                this.Opacity = this.Opacity - 0.1;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gb_null1.Enabled = rb_irc.Checked;
            gb_null2.Enabled = rb_rss.Checked; 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gb_null1.Enabled = rb_irc.Checked;
            gb_null2.Enabled = rb_rss.Checked; 
        }

        private void rb_nohm_CheckedChanged(object sender, EventArgs e)
        {
            rb_nohm.Checked = (!rb_irc.Checked && !rb_rss.Checked);
        }
 
    }
}

