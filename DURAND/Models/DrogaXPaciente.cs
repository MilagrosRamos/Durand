using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class DrogaXPaciente
    {
        public int      Id              { get; set; }
        public float    DosisEstandar   { get; set; }
        public float    DosisPaciente   { get; set; }
        public int      IdPaciente      { get; set; }
        public float    Peso            { get; set; }
        public float    Altura          { get; set; }
        public string   Observaciones   { get; set; }
        public int      IdDroga         { get; set; }

        public DrogaXPaciente()
        {
            Id              = 0;
            DosisEstandar   = 0;
            DosisPaciente   = 0;
            IdPaciente      = 0;
            Peso            = 0;
            Altura          = 0;
            Observaciones   = "";
            IdDroga         = 0;
        }
    }
}