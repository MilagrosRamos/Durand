using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class Medico
    {
        int         _idMedico;
        string      _nombre;
        string      _apellido;
        string      _foto;
        int         _especialidad;
        string      _mail;
        string      _contrasenia;

        

        [StringLength (8, ErrorMessage = "Es un DNI incorrecto")]
        int         _dni;

        public Medico()
        {

        }

        public Medico(int idMedico, string nombre, string apellido, string foto, int especialidad, string mail, string contrasenia, int dni)
        {
            _idMedico = idMedico;
            _nombre = nombre;
            _apellido = apellido;
            _foto = foto;
            _especialidad = especialidad;
            _mail = mail;
            _contrasenia = contrasenia;
            _dni = dni;
        }

        public int IdMedico
        {
            get
            {
                return _idMedico;
            }

            set
            {
                _idMedico = value;
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
        public string Apellido
        {
            get
            {
                return _apellido;
            }

            set
            {
                _apellido = value;
            }
        }
        public string Foto
        {
            get
            {
                return _foto;
            }

            set
            {
                _foto = value;
            }
        }
        public int Especialidad
        {
            get
            {
                return _especialidad;
            }

            set
            {
                _especialidad = value;
            }
        }
        public string Mail
        {
            get
            {
                return _mail;
            }

            set
            {
                _mail = value;
            }
        }
        public string Contrasenia
        {
            get
            {
                return _contrasenia;
            }

            set
            {
                _contrasenia = value;
            }
        }
        public int Dni
        {
            get
            {
                return _dni;
            }

            set
            {
                _dni = value;
            }
        }

    }
}