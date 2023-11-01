﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
    public static class Extensions
    {

        public static string GetEnumDisplayName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return value.ToString();
        }
    }
}
