using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class Estadistica
    {
        public int      Id { get; set; }
        public string   Nombre { get; set; }

        public Estadistica()
        {
            Id              = 0;
            Nombre          = "";
        }
    }
}