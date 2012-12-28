using System;
using System.Threading;
using System.Windows.Forms;
namespace GAS
{
	public partial class frm_main : Form
	{
		public frm_main() {
			InitializeComponent();
		}
		private void frm_main_Load(object sender, EventArgs e) {
		}
		#region Design by STAM
		private void btn_about_Click(object sender, EventArgs e) {
			frm_about frmabout = new frm_about();
			frmabout.ShowDialog();
		}
		private void button1_Click(object sender, EventArgs e) {
			frm_hive_mind frmhm = new frm_hive_mind();
			frmhm.ShowDialog();
		}
		private void frm_main_FormClosing(object sender, FormClosingEventArgs e) {
			for ( int i = 0; i < 10; i++ ) {
				Thread.Sleep(60);
				this.Opacity = this.Opacity - 0.1;
			}
		}
		#endregion
	}
}
