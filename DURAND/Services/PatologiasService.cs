using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DURAND.Helpers;
using DURAND.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace DURAND.Services
{
    public class PatologiasService
    {
        public static List<SelectListItem> ObtenerTodosDropDown()
        {
            List<SelectListItem> listaDevolver = new List<SelectListItem>();
            SelectListItem elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Patologias_ObtenerTodos");
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            elemento = new SelectListItem();
                            elemento.Value = Convert.ToString((currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0));
                            elemento.Text = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");

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

        public static Patologia ObtenerPorId(int intID)
        {
            Patologia returnEntity = null;
            SqlParameter[] parameterArray = new SqlParameter[1];
            SqlDataReader currentReader = null;

            parameterArray[0] = new SqlParameter("@intID", intID);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Patologias_ObtenerPorId", parameterArray);
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

        private static Patologia DataReaderToObject(SqlDataReader currentReader)
        {
            Patologia returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new Patologia();

                returnEntity.IdPatologias       = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.Nombre             = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
            }

            return returnEntity;
        }
    }
}