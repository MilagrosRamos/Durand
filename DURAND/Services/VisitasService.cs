using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DURAND.Helpers;
using DURAND.Models;

namespace DURAND.Services
{
    public class VisitasService
    {

        public static List<Visita> ObtenerPorIdPaciente(int intIdPaciente)
        {
            List<Visita> returnList = new List<Visita>();
            Visita returnEntity = null;
            SqlParameter[] parameterArray = new SqlParameter[1];
            SqlDataReader currentReader = null;

            parameterArray[0] = new SqlParameter("@intIdPaciente", intIdPaciente);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Visitas_ObtenerPorIdPaciente", parameterArray);
                if (currentReader != null)
                {

                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            returnEntity = DataReaderToObject(currentReader);
                            returnEntity = DataReaderToObjectExtendido(currentReader, returnEntity);
                            returnList.Add(returnEntity);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }

            DatabaseHelper.CloseAndDisposeReader(ref currentReader);

            return returnList;
        }

        public static int AgregarVisita(Visita unaVisita)
        {
            int intRegsAffected = 0;

            SqlParameter[] parameterArray = new SqlParameter[6];

            parameterArray[0] = new SqlParameter("@FechaVisita",    unaVisita.FechaVisita);
            parameterArray[1] = new SqlParameter("@Peso",           unaVisita.Peso);
            parameterArray[2] = new SqlParameter("@Altura",         unaVisita.Altura);
            parameterArray[3] = new SqlParameter("@Observaciones",  unaVisita.Observaciones);
            parameterArray[4] = new SqlParameter("@IDPaciente",     unaVisita.IDPaciente);
            parameterArray[5] = new SqlParameter("@IDMedico",       unaVisita.IDMedico);

            try
            {
                intRegsAffected = DatabaseHelper.ExecuteNonQuery("Visitas_AgregarVisita", parameterArray);
                
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }
            return intRegsAffected;
        }

        private static Visita DataReaderToObject(SqlDataReader currentReader)
        {
            Visita returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new Visita();

                returnEntity.Id = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.FechaVisita = (currentReader["FechaVisita"] != DBNull.Value ? (DateTime)currentReader["FechaVisita"] : DateTime.MinValue);
                returnEntity.Peso = (currentReader["Peso"] != DBNull.Value ? Convert.ToSingle(currentReader["Peso"]) : 0);
                returnEntity.Altura = (currentReader["Altura"] != DBNull.Value ? Convert.ToSingle(currentReader["Altura"]) : 0);
                returnEntity.Observaciones = (currentReader["Observaciones"] != DBNull.Value ? (string)(currentReader["Observaciones"]) : "");
                returnEntity.IDMedico = (currentReader["IDMedico"] != DBNull.Value ? (int)currentReader["IDMedico"] : 0);
                returnEntity.IDPaciente = (currentReader["IDPaciente"] != DBNull.Value ? (int)currentReader["IDPaciente"] : 0);
            }

            return returnEntity;
        }

        private static Visita DataReaderToObjectExtendido(SqlDataReader currentReader, Visita returnEntity)
        {
            if (currentReader != null)
            {

                returnEntity.MedicoDescripcion= (currentReader["MedicoDescripcion"] != DBNull.Value ? (string)currentReader["MedicoDescripcion"] : "");
            }

            return returnEntity;
        }


    }
}