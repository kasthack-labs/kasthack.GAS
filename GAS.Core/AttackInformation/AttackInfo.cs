using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core
{
	[Serializable]
	public class AttackInfo
	{
		public AttackParam[] Params;
		string AttackName { get; set; }
	}
}
