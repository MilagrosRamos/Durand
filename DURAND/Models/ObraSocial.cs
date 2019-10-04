using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class ObraSocial
    {
        int _idObraSocial;
        string _nombre;

        public ObraSocial()
        {

        }

        public ObraSocial(int idObraSocial, string nombre)
        {
            _idObraSocial = idObraSocial;
            _nombre = nombre;
        }

        public int IdObraSocial
        {
            get
            {
                return _idObraSocial;
            }

            set
            {
                _idObraSocial = value;
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