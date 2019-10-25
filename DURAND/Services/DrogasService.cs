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
    public class DrogasService
    {
        public static List<SelectListItem> ObtenerTodosDropDown()
        {
            List<SelectListItem> listaDevolver = new List<SelectListItem>();
            SelectListItem elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Drogas_ObtenerTodas");
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            elemento            = new SelectListItem();
                            elemento.Value      = Convert.ToString((currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0));
                            elemento.Text       = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
                            //elemento.       = (currentReader["DosisStandard"] != DBNull.Value ? (string)currentReader["DosisStandard"] : "");

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

        public static int AgregarDroga(Droga unaDroga)
        {
            
            int intRegsAffected = 0;

            SqlParameter[] parameterArray = new SqlParameter[7];

            //parameterArray[0] = new SqlParameter("@Id",             unaDroga.FechaVisita);
            //parameterArray[1] = new SqlParameter("@DosisEstandar",  unaDroga.Peso);
            //parameterArray[2] = new SqlParameter("@DosisPaciente",  unaDroga.Altura);
            //parameterArray[3] = new SqlParameter("@IDPaciente",     unaDroga.Observaciones);
            //parameterArray[4] = new SqlParameter("@Peso",           unaDroga.IDPaciente);
            //parameterArray[5] = new SqlParameter("@Altura",         unaDroga.IDMedico);
            //parameterArray[6] = new SqlParameter("@Observaciones",  unaDroga.IDMedico);
            //parameterArray[7] = new SqlParameter("@IDDroga",        unaDroga.IDMedico);

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

        private static Droga DataReaderToObject(SqlDataReader currentReader)
        {
            Droga returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new Droga();
              
                returnEntity.Id             = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.Nombre         = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
                returnEntity.DosisStandard = (currentReader["DosisStandard"] != DBNull.Value ? (string)currentReader["DosisStandard"] : "");
            }

            return returnEntity;
        }

    }
}