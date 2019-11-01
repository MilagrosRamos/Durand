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
            SelectListItem  elemento = null;
            String          strNombre, strDosis;
            double          dblDosis;

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
                            strNombre           = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
                            dblDosis            = (currentReader["DosisStandard"] != DBNull.Value ? (double)currentReader["DosisStandard"] : 0);
                            strDosis            = dblDosis.ToString("0.00");
                            elemento.Text       = string.Format ("{0} - {1} mg/m2 ",strNombre , strDosis);
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