using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DURAND.Helpers
{
    /// <summary>
    ///     Clase que ayuda a obtener los datos de los archivos de configuracion de la aplicacion.
    /// </summary>
    public class AppSettingsHelper {

        /// <summary>
        ///     Obtiene de la clave "key" del archivo de configuracion, en caso de no existir retorna "defaultValue".
        /// </summary>
        /// <param name="key">Clave</param>
        /// <param name="defaultValue">Valor a devolver en caso de no existir.</param>
        /// <returns></returns>
        public static string GetAppSetting(string key, string defaultValue) {
            string strReturnValue = defaultValue;
            string strValue;

            strValue = ConfigurationManager.AppSettings[key];
            if (strValue != null) {
                strReturnValue = strValue;
            }
            return strReturnValue;
        }
    }
}
