using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Func
{
    public class WriteLogFile
    {
        public static bool WriteLog(string strFileName, List<string> strMessage, string folder)
        {
            try
            {
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "Logs/" + folder, strFileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                foreach (var item in strMessage)
                {
                    objStreamWriter.WriteLine(item);
                }
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool WriteLog(string strFileName, string strMessage, string folder)
        {
            try
            {
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "Logs/" + folder, strFileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
