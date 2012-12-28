using System;
using System.Windows.Forms;
namespace GAS
{
	public partial class frmmain : Form
	{
		GAS.Core.Manager Core = new GAS.Core.Manager();
		#region Flags
		bool IPOK = false, Attacking = false;
		#endregion
		public frmmain() {
			InitializeComponent();
		}
		private void Error(bool Fatal, string Error) {
			MessageBox.Show(Error, ( Fatal ? "Fatal " : "" ) + "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		private void cmdTargetIP_Click(object sender, EventArgs e) {
			try {
				IPOK = Core.LockOn(txtTargetIP.Text);
				if ( IPOK ) {
					txtTarget.Text = Core.Target.ToString();
					txtSubsite.Text = Core.Subsite;
				}
			}
			catch ( Exception ex ) {
				IPOK = false;
				txtTarget.Text = "N O N E !";
				txtTargetIP.Focus();
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void cbMethod_SelectedIndexChanged(object sender, EventArgs e) {
			LockSetting(false);
		}
		private void cmdAttack_Click(object sender, EventArgs e) {
			if ( Attacking ^= true )
				if ( IPOK )
					StartAttack();
				else Error(true, "U didn't specified target");
			else StopAttack();
		}
		void StopAttack() {
			Core.Stop();
			Attacking = false;
			cmdAttack.Text = "IMMA CHARGIN MAH LAZER";
			LockSetting(Attacking);
		}
		void StartAttack() {
			#region Configure
			Core.Subsite = txtSubsite.Text;
			Core.Method = (GAS.Core.AttackMethod)Enum.Parse(typeof(GAS.Core.AttackMethod), cbMethod.Text);
			Core.Port = Convert.ToInt32(nudPort.Value);
			Core.Timeout = Convert.ToInt32(nudTimeout.Value);
			Core.Threads = Convert.ToInt32(nudThreadNum.Value);
			Core.SPT = Convert.ToInt32(nudSLPT.Value);
			Core.USEGet = chkUseGet.Checked;
			Core.UseGZIP = chkUsegZip.Checked;
			Core.Delay = tbSpeed.Value;
			Core.WaitForResponse = chkResp.Checked;
			Core.AppendRANDOMChars = chkMsgRandom.Checked;
			Core.AppendRANDOMCharsUrl = chkRandom.Checked;
			Core.Data = txtData.Text;
			#endregion
			cmdAttack.Text = "Stop Attack";
			Application.DoEvents();
			LockSetting(Attacking);
			Core.Start();
		}
		void LockSetting(bool ForAttack) {
			/*
			cbMethod.Enabled = !ForAttack;
			chkUsegZip.Enabled = ForAttack ? false : (cbMethod.SelectedIndex >= 2);
			chkUseGet.Enabled = ForAttack ? false : (cbMethod.SelectedIndex == 4);
			chkMsgRandom.Enabled =ForAttack? false:(cbMethod.SelectedIndex <= 1);
			txtData.Enabled = ForAttack ? false : (cbMethod.SelectedIndex <= 1);
			chkRandom.Enabled = ForAttack ? false : (cbMethod.SelectedIndex >= 2);
			txtSubsite.Enabled = ForAttack ? false : (cbMethod.SelectedIndex >= 2);
			chkResp.Enabled = ForAttack ? false : !(cbMethod.SelectedIndex == 4);
			nudSLPT.Enabled = ForAttack? false:(cbMethod.SelectedIndex >= 3);
			nudTimeout.Enabled = !ForAttack;
			nudPort.Enabled = !ForAttack;
			nudThreadNum.Enabled = !ForAttack;
			tbSpeed.Enabled = !ForAttack;
			* */
		}
		private void tShowStats_Tick(object sender, EventArgs e) {
			lbDownloaded.Text = Core.Worker.Downloaded.ToString();
			lbFailed.Text = Core.Worker.Failed.ToString();
		}
	}
}
