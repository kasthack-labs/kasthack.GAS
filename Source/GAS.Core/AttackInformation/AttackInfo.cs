using System;

namespace GAS.Core.AttackInformation {
    [Serializable]
    public class AttackInfo {
        public AttackParam[] Params;
        string AttackName { get; set; }
    }
}
