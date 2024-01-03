using HRM.SC.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ModelProject.Func
{
	public static class EncryptionSecurity
	{
		public static string DecryptV2(string input)
		{
			if(string.IsNullOrEmpty(input)) return string.Empty;
			var outPut = Encryption.DecryptV2(input);
			return outPut;
		}
		public static string EncryptV2(string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;
			var outPut = Encryption.EncryptV2(input);
			return outPut;
		}
		public static string Base64String(string intput)
		{
			try
			{
				Convert.FromBase64String(intput);
				var outPut = EncryptionSecurity.DecryptV2(intput);
				return outPut;
			}
			catch (FormatException)
			{
				return intput;
			}
		}
	}
}
