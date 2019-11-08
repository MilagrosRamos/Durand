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
    public class EstadisticasService
    {
        public static List<SelectListItem> ObtenerTodosDropDown()
        {
            List<SelectListItem> listaDevolver = new List<SelectListItem>();
            SelectListItem elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Estadisticas_ObtenerTodos");
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


        public static List<EstadisticaFiltrada> VisitasXMes()
        {
            List<EstadisticaFiltrada> listaDevolver = new List<EstadisticaFiltrada>();
            EstadisticaFiltrada elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Estadisticas_VisitasXMes");
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            elemento = new EstadisticaFiltrada();
                            elemento.Nombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
                            elemento.Valor = Convert.ToInt32((currentReader["Valor"] != DBNull.Value ? (int)currentReader["Valor"] : 0));

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

        public static List<EstadisticaFiltrada> PacientesXObraSocial()
        {
            List<EstadisticaFiltrada> listaDevolver = new List<EstadisticaFiltrada>();
            EstadisticaFiltrada elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Estadisticas_PacientesXObraSocial");
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            elemento = new EstadisticaFiltrada();
                            elemento.Nombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
                            elemento.Valor = Convert.ToInt32((currentReader["Valor"] != DBNull.Value ? (int)currentReader["Valor"] : 0));

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

        public static List<EstadisticaFiltrada> PacientesxPatologias()
        {
            List<EstadisticaFiltrada> listaDevolver = new List<EstadisticaFiltrada>();
            EstadisticaFiltrada elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Estadisticas_PacientesxPatologias");
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            elemento = new EstadisticaFiltrada();
                            elemento.Nombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
                            elemento.Valor = Convert.ToInt32((currentReader["Valor"] != DBNull.Value ? (int)currentReader["Valor"] : 0));

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

        private static Estadistica DataReaderToObject(SqlDataReader currentReader)
        {
            Estadistica returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new Estadistica();

                returnEntity.Id = (currentReader["Id"] != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.Nombre = (currentReader["Nombre"] != DBNull.Value ? (string)currentReader["Nombre"] : "");
            }

            return returnEntity;
        }
    }
}