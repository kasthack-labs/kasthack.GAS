using System;
using System.Threading;
using System.Windows.Forms;
namespace GAS
{
	public partial class frm_hive_mind : Form
	{
		public frm_hive_mind() {
			InitializeComponent();
		}
		private void frm_hive_mind_Load(object sender, EventArgs e) {
			for ( int i = 0; i < 10; i++ ) {
				Thread.Sleep(60);
				this.Opacity = this.Opacity + 0.1;
			}
		}
		private void frm_hive_mind_FormClosing(object sender, FormClosingEventArgs e) {
			for ( int i = 0; i < 10; i++ ) {
				Thread.Sleep(60);
				this.Opacity = this.Opacity - 0.1;
			}
		}
		private void radioButton1_CheckedChanged(object sender, EventArgs e) {
			gb_null1.Enabled = rb_irc.Checked;
			gb_null2.Enabled = rb_rss.Checked;
		}
		private void radioButton2_CheckedChanged(object sender, EventArgs e) {
			gb_null1.Enabled = rb_irc.Checked;
			gb_null2.Enabled = rb_rss.Checked;
		}
		private void rb_nohm_CheckedChanged(object sender, EventArgs e) {
			rb_nohm.Checked = ( !rb_irc.Checked && !rb_rss.Checked );
		}
	}
}
