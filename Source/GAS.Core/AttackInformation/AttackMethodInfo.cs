using System;

namespace GAS.Core.AttackInformation {
    [Serializable]
    public class AttackMethodInfo    {
        public AttackParam[] Params;
        private string AttackName { get; set; }
    }
}