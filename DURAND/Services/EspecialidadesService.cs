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
    public class EspecialidadesService
    {
        public static List<SelectListItem> ObtenerTodosDropDown()
        {
            List<SelectListItem> listaDevolver = new List<SelectListItem>();
            SelectListItem elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Especialidades_ObtenerTodos");
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

        public static Especialidades ObtenerPorId(int intID)
        {
            Especialidades returnEntity = null;
            SqlParameter[] parameterArray = new SqlParameter[1];
            SqlDataReader currentReader = null;

            parameterArray[0] = new SqlParameter("@intID", intID);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Especialidades_ObtenerPorId", parameterArray);
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

        private static Especialidades DataReaderToObject(SqlDataReader currentReader)
        {
            Especialidades returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new Especialidades();

                returnEntity.Id     = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.Nombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
            }

            return returnEntity;
        }
    }
}