using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DURAND.Helpers {
    /// <summary>
    ///     Clase que agrupa funcionalidades de lectura y escritura de archivos.
    /// </summary>
    public class IOHelper {
        /// <summary>
        ///     Crea y guarda en el archivo de texto especificado los datos enviados.
        /// </summary>
        /// <param name="strPathFile"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static bool WriteToFile(string strPathFile, string strData) {
            return WriteToFileInternal(strPathFile, strData, FileMode.Create);
        }

        //// <summary>
        ///     Agrega en el archivo de texto especificado los datos enviados.
        /// </summary>
        /// <param name="strPathFile"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static bool AppendToFile(string strPathFile, string strData) {
            return WriteToFileInternal(strPathFile, strData, FileMode.Append);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPathFile"></param>
        /// <param name="strData"></param>
        /// <param name="fileMode"></param>
        /// <returns></returns>
        private static bool WriteToFileInternal(string strPathFile, string strData, FileMode fileMode) {
            bool blnReturnValue = false;
            try {
                using (FileStream fs = new FileStream(strPathFile, fileMode, FileAccess.Write, FileShare.ReadWrite)) {
                    using (StreamWriter sw = new StreamWriter(fs)) {
                        sw.Write(strData);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
                blnReturnValue = true;
            } catch {

            }
            return blnReturnValue;
        }

    }
}
