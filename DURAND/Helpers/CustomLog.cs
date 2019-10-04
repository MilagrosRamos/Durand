using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DURAND.Models;

namespace DURAND.Helpers {
    public class CustomLog {
        public static string GetLogFile() {
            string strReturnValue;
            strReturnValue = AppSettingsHelper.GetAppSetting("LogFile", "");
            return strReturnValue;
        }

        public static bool LogException(Exception ex) {
            bool blnReturnValue = false;

            blnReturnValue = LogException(ex.Message);

            return blnReturnValue;
        }

        public static bool LogException(string strData) {
            bool    blnReturnValue = false;
            string  strPathFile;
            string  strTextToLog;

            strPathFile = CustomLog.GetLogFile();

            if (!string.IsNullOrEmpty(strPathFile)) {
                strTextToLog = string.Format("{0} {1}{2}",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    strData,
                    Environment.NewLine);

                blnReturnValue = IOHelper.AppendToFile(strPathFile, strTextToLog);
            }
            return blnReturnValue;
        }
    }
}
