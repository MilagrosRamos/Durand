using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class Droga
    {
        public int      Id { get; set; }
        public string   Nombre { get; set; }
        public string   DosisStandard { get; set; }

        public Droga()
        {
            Id              = 0;
            Nombre          = "";
            DosisStandard   = "";

        }
    }
}