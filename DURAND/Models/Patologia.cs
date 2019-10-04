using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class Patologia
    {
        int _idPatologia;
        string _nombre;

        public Patologia()
        {

        }

        public Patologia(int idPatologia, string nombre)
        {
            _idPatologia = idPatologia;
            _nombre = nombre;
        }

        public int IdPatologias
        {
            get
            {
                return _idPatologia;
            }

            set
            {
                _idPatologia = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _nombre;
            }

            set
            {
                _nombre = value;
            }
        }
    }
}