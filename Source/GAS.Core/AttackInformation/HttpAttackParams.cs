using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core.AttackInformation {
    public class HttpAttackParams {
        public string Dns { get; set; }
        public string Query { get; set; }
        public bool WaitForResponse { get; set; }
        public HttpAttackType AttackType { get; set; }
        public bool RandomAppendUrl { get; set; }
        public bool RandomAppendHost { get; set; }
        public bool Gzip { get; set; }
    }

    public enum HttpAttackType {
        Deafult,
        RefRef,
        AhrDosme
    }
}
