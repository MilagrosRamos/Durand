using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DURAND.Models
{
    public class Paciente
    {

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Se requiere un nombre")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Se requiere un apellido")]
        public string Apellido { get; set; }
        [Required]
        public int IDSexo { get; set; }
        [Required]
        public int IDObraSocial { get; set; }
        [Required]
        public string Domicilio { get; set; }
        public int Telefono { get; set; }
        public int IDArchivo { get; set; }
        [Required]
        public DateTime FechaDeNacimiento { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public int IDProvincia { get; set; }
        public int IDLocalidad { get; set; }
        public string RutaFoto { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Es un DNI incorrecto")]
        public string DNI { get; set; }
        public int IDPatologia { get; set; }

        public Paciente()
        {
            Id                      = 0;
            Nombre                  = "";
            Apellido                = "";
            IDSexo                  = 0;
            IDObraSocial            = 0;
            Domicilio               = "";
            Telefono                = 0;
            IDArchivo               = 0;
            FechaDeNacimiento       = DateTime.MinValue;
            Altura                  = 0;
            Peso                    = 0;
            IDProvincia             = 0;
            IDLocalidad             = 0;
            RutaFoto                = "";
            DNI                     = "";
            IDPatologia             = 0;
        }
    }
}