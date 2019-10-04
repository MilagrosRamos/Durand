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
    public class ProvinciasService
    {
        public static List<SelectListItem> ObtenerTodosDropDown()
        {
            List<SelectListItem> listaDevolver = new List<SelectListItem>();
            SelectListItem elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Provincias_ObtenerTodos");
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
    }
}