using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Helpers
{
    public class FormatosHelper
    {
        public static string FormatearFecha(object oFecha)
        {
            DateTime dtmFecha = Convert.ToDateTime(oFecha);
            return FormatearFecha(dtmFecha);
        }

        public static string FormatearFecha(DateTime dtmFecha)
        {
            return dtmFecha.ToString("dd-MM-yyyy");
        }
    }
}