using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
namespace GAS
{
    public partial class frm_about : Form
    {
        public frm_about() {
            InitializeComponent();
            this.Width = 218;
            this.Height = 0;

        }
        private void timer1_Tick(object sender, EventArgs e) {
            if ( this.Height < 519 ) {
                rsz(8);
                return;
            }
            if ( this.Width < 600 ) {
                this.Width += 8;
                return;
            }
            timer.Stop();
        }
        private void frm_about_Load(object sender, EventArgs e) {
            lbl_progv.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lbl_proc.Text = (char)9830 + " Count of CP: " + Environment.ProcessorCount.ToString();
            lbl_clr.Text = (char)9830 + " CLR Version: .NET " + Environment.Version.ToString();
            lbl_username.Text = (char)9830 + " Username: " + Environment.UserName.ToString();
            lbl_gas64.Text = (char)9830 + " 64-bit app: " + Environment.Is64BitProcess.ToString();
            lbl_pagesize.Text = (char)9830 + " Page Size: " + Environment.SystemPageSize.ToString() + " Mb";
            lbl_wssize.Text = (char)9830 + " Physical Memory: " + Environment.WorkingSet.ToString() + " Mb";
            lbl_os64.Text = (char)9830 + " 64-bit OS: " + Environment.Is64BitOperatingSystem.ToString();
            lbl_os.Text = (char)9830 + " OS Version: " + Environment.OSVersion.ToString();
        }
        void rsz(int diff) {
            this.Height += diff;
            this.Location = new Point(this.Location.X - diff / 2,
                this.Location.Y - diff / 2);
        }
        private void frm_about_FormClosing(object sender, FormClosingEventArgs e) {
            for ( int i = 0; i < 10; i++ ) {
                Thread.Sleep(60);
                this.Opacity = this.Opacity - 0.1;
            }
        }
        private void link_support_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/kasthack/GAS");
        }

        private void btn_close_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
