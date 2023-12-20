using HRM.SC.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Func
{
	public static class EncryptionSecurity
	{
		public static string DecryptV2(string input)
		{
			var outPut = Encryption.DecryptV2(input);
			return outPut;
		}
		public static string EncryptV2(string input)
		{
			var outPut = Encryption.EncryptV2(input);
			return outPut;
		}
	}
}
