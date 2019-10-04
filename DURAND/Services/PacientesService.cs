using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DURAND.Helpers;
using DURAND.Models;

namespace DURAND.Services {
    public class PacientesService {
        
        public static Paciente ObtenerPorDNI(string strDNI) {
            Paciente        returnEntity = null;
            SqlParameter[]  parameterArray = new SqlParameter[1];
            SqlDataReader   currentReader = null;

            parameterArray[0] = new SqlParameter("@strDNI", strDNI);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Pacientes_ObtenerPorDNI", parameterArray);
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        currentReader.Read();
                        returnEntity = DataReaderToObject(currentReader);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }

            DatabaseHelper.CloseAndDisposeReader(ref currentReader);

            return returnEntity;
        }

        public static Paciente ObtenerPorId(int intID)
        {
            Paciente returnEntity = null;
            SqlParameter[] parameterArray = new SqlParameter[1];
            SqlDataReader currentReader = null;

            parameterArray[0] = new SqlParameter("@intID", intID);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Pacientes_ObtenerPorId", parameterArray);
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        currentReader.Read();
                        returnEntity = DataReaderToObject(currentReader);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }

            DatabaseHelper.CloseAndDisposeReader(ref currentReader);

            return returnEntity;
        }

        public static int AgregarPaciente(Paciente unPaciente)
        {
            int intRegsAffected = 0;
            SqlParameter[] parameterArray = new SqlParameter[15];

            parameterArray[0] =     new SqlParameter("@Nombre",             unPaciente.Nombre);
            parameterArray[1] =     new SqlParameter("@Apellido",           unPaciente.Apellido);
            parameterArray[2] =     new SqlParameter("@IDSexo",             unPaciente.IDSexo);
            parameterArray[3] =     new SqlParameter("@IDObraSocial",       unPaciente.IDObraSocial);
            parameterArray[4] =     new SqlParameter("@Domicilio",          unPaciente.Domicilio);
            parameterArray[5] =     new SqlParameter("@Telefono",           unPaciente.Telefono);
            parameterArray[6] =     new SqlParameter("@IDArchivo",          unPaciente.IDArchivo);
            parameterArray[7] =     new SqlParameter("@FechaDeNacimiento",  unPaciente.FechaDeNacimiento);
            parameterArray[8] =     new SqlParameter("@Altura",             unPaciente.Altura);
            parameterArray[9] =     new SqlParameter("@Peso",               unPaciente.Peso);
            parameterArray[10] =    new SqlParameter("@IDProvincia",        unPaciente.IDProvincia);
            parameterArray[11] =    new SqlParameter("@IDLocalidad",        unPaciente.IDLocalidad);
            parameterArray[12] =    new SqlParameter("@RutaFoto",           unPaciente.RutaFoto);
            parameterArray[13] =    new SqlParameter("@DNI",                unPaciente.DNI);
            parameterArray[14] =    new SqlParameter("@IDPatologia",        unPaciente.IDPatologia);
            try
            {
                intRegsAffected = DatabaseHelper.ExecuteNonQuery("Pacientes_AgregarPaciente", parameterArray);

            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }
            return intRegsAffected;
        }

        private static Paciente DataReaderToObject(SqlDataReader currentReader)
        {
            Paciente returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new Paciente();

                returnEntity.Id                 = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.Nombre             = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
                returnEntity.Apellido           = (currentReader["Apellido"] != DBNull.Value ? (string)currentReader["Apellido"] : "");
                returnEntity.IDSexo             = (currentReader["IDSexo"] != DBNull.Value ? (int)currentReader["IDSexo"] : 0);
                returnEntity.IDObraSocial       = (currentReader["IDObraSocial"] != DBNull.Value ? (int)currentReader["IDObraSocial"] : 0);
                returnEntity.Domicilio          = (currentReader["Domicilio"] != DBNull.Value ? (string)currentReader["Domicilio"] : "");
                returnEntity.Telefono           = (currentReader["Telefono"] != DBNull.Value ? (int)currentReader["Telefono"] : 0);
                returnEntity.IDArchivo          = (currentReader["IDArchivo"] != DBNull.Value ? (int)currentReader["IDArchivo"] : 0);
                returnEntity.FechaDeNacimiento  = (currentReader["FechaDeNacimiento"] != DBNull.Value ? (DateTime)currentReader["FechaDeNacimiento"] : DateTime.MinValue);
                returnEntity.Altura             = (currentReader["Altura"] != DBNull.Value ? Convert.ToSingle(currentReader["Altura"]) : 0);
                returnEntity.Peso               = (currentReader["Peso"] != DBNull.Value ? Convert.ToSingle(currentReader["Peso"]) : 0);
                returnEntity.IDProvincia        = (currentReader["IDProvincia"] != DBNull.Value ? (int)currentReader["IDProvincia"] : 0);
                returnEntity.IDLocalidad        = (currentReader["IDLocalidad"] != DBNull.Value ? (int)currentReader["IDLocalidad"] : 0);
                returnEntity.RutaFoto           = (currentReader["RutaFoto"] != DBNull.Value ? (string)currentReader["RutaFoto"] : "");
                returnEntity.DNI                = (currentReader["DNI"] != DBNull.Value ? (string)currentReader["DNI"] : "");
                returnEntity.IDPatologia        = (currentReader["IDPatologia"] != DBNull.Value ? (int)currentReader["IDPatologia"] : 0);
            }

            return returnEntity;
        }

    }
}