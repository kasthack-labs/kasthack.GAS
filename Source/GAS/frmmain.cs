using System;
using System.Windows.Forms;
using GAS.Core;
using GAS.Core.AttackInformation;

namespace GAS {
    public partial class Frmmain : Form {
        readonly Manager _core = new Manager();
        #region Flags
        bool _ipok, _attacking;
        #endregion
        public Frmmain() {
            InitializeComponent();
        }
        private void Error( bool fatal, string error ) {
            MessageBox.Show( error, ( fatal ? "Fatal " : "" ) + @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
        private void cmdTargetIP_Click( object sender, EventArgs e ) {
            try {
                this._ipok = this._core.LockOn( txtTargetIP.Text );
                if ( this._ipok ) {
                    txtTarget.Text = this._core.Target.ToString();
                    txtSubsite.Text = this._core.Subsite;
                }
            }
            catch ( Exception ex ) {
                this._ipok = false;
                txtTarget.Text = @"N O N E !";
                txtTargetIP.Focus();
                MessageBox.Show( ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }
        private void cbMethod_SelectedIndexChanged( object sender, EventArgs e ) {
            LockSetting( false );
        }
        private void cmdAttack_Click( object sender, EventArgs e ) {
            if ( this._attacking ^= true )
                if ( this._ipok )
                    StartAttack();
                else Error( true, "U didn't specified target" );
            else StopAttack();
        }
        void StopAttack() {
            this._core.Stop();
            this._attacking = false;
            cmdAttack.Text = @"IMMA CHARGIN MAH LAZER";
            LockSetting( this._attacking );
        }
        void StartAttack() {
            #region Configure
            this._core.Subsite = txtSubsite.Text;
            //this._core.Method = (AttackMethod) Enum.Parse( typeof( AttackMethod ), cbMethod.Text );
            this._core.Port = Convert.ToInt32( nudPort.Value );
            this._core.Timeout = Convert.ToInt32( nudTimeout.Value );
            this._core.Threads = Convert.ToInt32( nudThreadNum.Value );
            this._core.Spt = Convert.ToInt32( nudSLPT.Value );
            this._core.UseGet = chkUseGet.Checked;
            this._core.UseGZIP = chkUsegZip.Checked;
            this._core.Delay = tbSpeed.Value;
            this._core.WaitForResponse = chkResp.Checked;
            this._core.AppendRandomChars = chkMsgRandom.Checked;
            this._core.AppendRandomCharsUrl = chkRandom.Checked;
            this._core.Data = txtData.Text;
            #endregion
            cmdAttack.Text = @"Stop Attack";
            Application.DoEvents();
            LockSetting( this._attacking );
            this._core.Start();
        }
        void LockSetting( bool forAttack ) {
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
        private void tShowStats_Tick( object sender, EventArgs e ) {
            lbDownloaded.Text = this._core.Worker.Downloaded.ToString();
            lbFailed.Text = this._core.Worker.Failed.ToString();
        }
    }
}
