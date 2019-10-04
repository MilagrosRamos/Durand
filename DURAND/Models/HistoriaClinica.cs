using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class HistoriaClinica
    {
        public List<Visita> visitas { get; set; }
        public Paciente unPaciente { get; set; }
        public Visita unaVisita { get; set; }

        private void CargarVisitas()
        {
            visitas = new List<Visita>();

        }
        private void CargarPaciente()
        {
            
        }

        public HistoriaClinica()
        {
            CargarVisitas();
            CargarPaciente();
        }
    }
}