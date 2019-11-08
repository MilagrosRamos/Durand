using DURAND.Helpers;
using DURAND.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DURAND.Services
{
    public class DrogaXPacienteService
    {

        public static int AgregarDroga(DrogaXPaciente unaDroga)
        {
            int intRegsAffected = 0;

            SqlParameter[] parameterArray = new SqlParameter[7];

            parameterArray[0] = new SqlParameter("@DosisEstandar",  unaDroga.DosisEstandar);
            parameterArray[1] = new SqlParameter("@DosisPaciente",  unaDroga.DosisPaciente);
            parameterArray[2] = new SqlParameter("@IDPaciente",     unaDroga.IdPaciente);
            parameterArray[3] = new SqlParameter("@Peso",           unaDroga.Peso);
            parameterArray[4] = new SqlParameter("@Altura",         unaDroga.Altura);
            parameterArray[5] = new SqlParameter("@Observaciones",  unaDroga.Observaciones);
            parameterArray[6] = new SqlParameter("@IDDroga",        unaDroga.IdDroga);

            try
            {
                intRegsAffected = DatabaseHelper.ExecuteNonQuery("DrogaXPaciente_Agregar", parameterArray);

            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }
            return intRegsAffected;
        }

        public static List<DrogaXPaciente> ObtenerPorId(int id)
        {
            List<DrogaXPaciente> listaDevolver = new List<DrogaXPaciente>();
            DrogaXPaciente elemento = null;
            SqlParameter[] parameterArray = new SqlParameter[1];
            SqlDataReader currentReader = null;

            parameterArray[0] = new SqlParameter("@id", id);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("DrogaXPaciente_ObtenerPorId", parameterArray);
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            elemento                        = new DrogaXPaciente();
                            elemento.Id                     = Convert.ToInt32 ((currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0));
                            elemento.DosisEstandar          = Convert.ToSingle((currentReader["DosisEstandar"] != DBNull.Value ? (float)currentReader["DosisEstandar"] : 0));
                            elemento.DosisPaciente          = Convert.ToSingle((currentReader["DosisPaciente"] != DBNull.Value ? (float)currentReader["DosisPaciente"] : 0));
                            elemento.IdPaciente             = Convert.ToInt32((currentReader["IDPaciente"] != DBNull.Value ? (int)currentReader["IDPaciente"] : 0));
                            elemento.Peso                   = Convert.ToSingle((currentReader["Peso"] != DBNull.Value ? (float)currentReader["Peso"] : 0));
                            elemento.Altura                 = Convert.ToSingle((currentReader["Altura"] != DBNull.Value ? (float)currentReader["Altura"] : 0));
                            elemento.Observaciones          = (currentReader["Observaciones"] != DBNull.Value ? (string)currentReader["Observaciones"] : "");
                            elemento.IdDroga                = Convert.ToInt32((currentReader["IDDroga"] != DBNull.Value ? (int)currentReader["IDDroga"] : 0));
                            listaDevolver.Add(elemento);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }

            DatabaseHelper.CloseAndDisposeReader(ref currentReader);

            return listaDevolver;
        }

        private static DrogaXPaciente DataReaderToObject(SqlDataReader currentReader)
        {
            DrogaXPaciente returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new DrogaXPaciente();

                returnEntity.Id     = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                //returnEntity.Nombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
            }

            return returnEntity;
        }

    }
}