namespace GAS.Core.Attacks {
    public class SslInfo {
        private bool _useSsl;
        private string _domain;
        /// <summary>
        /// Is SSL enabled
        /// </summary>
        public bool UseSsl {
            get { return this._useSsl; }
            set { this._useSsl = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Domain {
            get { return this._domain; }
            set { this._domain = value; }
        }

    }
}