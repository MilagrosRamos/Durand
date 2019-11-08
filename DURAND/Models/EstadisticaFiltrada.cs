using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class EstadisticaFiltrada
    {
        public string   Nombre { get; set; }
        public int      Valor { get; set; }

        public EstadisticaFiltrada()
        {
            Nombre = "";
            Valor = 0; 
        }
    }
}