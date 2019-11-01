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

        public static List<SelectListItem> ObtenerPorId(int id)
        {
            List<SelectListItem> listaDevolver = new List<SelectListItem>();
            SelectListItem elemento = null;


            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("DrogaXPaciente_ObtenerPorId");
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            elemento = new SelectListItem();
                            elemento.Value = Convert.ToString((currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0));
                            
                            
                            //strNombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
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

        private static Especialidades DataReaderToObject(SqlDataReader currentReader)
        {
            Especialidades returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new DrogaXPaciente();

                returnEntity.Id     = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.Nombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
            }

            return returnEntity;
        }

    }
}