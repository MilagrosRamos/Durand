using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class Visita
    {
        public int          Id { get; set; }
        public DateTime     FechaVisita { get; set; }
        public float        Peso { get; set; }
        public float        Altura { get; set; }
        public string       Observaciones { get; set; }
        public int          IDPaciente { get; set; }
        public int          IDMedico { get; set; }
        public string       MedicoDescripcion { get; set; }

        public Visita()
        {
            Id = 0;
            FechaVisita = DateTime.MinValue;
            Altura = 0;
            Peso = 0;
            Observaciones = "";
            IDPaciente = 0;
            IDMedico = 0;

            MedicoDescripcion = "";
    }

    }
}