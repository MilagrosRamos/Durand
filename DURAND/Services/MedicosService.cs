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
    public class MedicosService
    {
        public static List<Medico> ObtenerTodos()
        {
            List<Medico> listaMedicos = new List<Medico>();
            Medico medicoAux = null;
            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Medicos_ObtenerTodos");
                if (currentReader != null)
                {
                    if (currentReader.HasRows)
                    {
                        while (currentReader.Read())
                        {
                            medicoAux = DataReaderToObject(currentReader);
                            listaMedicos.Add(medicoAux);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }

            DatabaseHelper.CloseAndDisposeReader(ref currentReader);

            return listaMedicos;
        }

        public static Medico ObtenerPorMailyContrasenia(string strMail, string strContrasenia)
        {
            Medico returnEntity = null;
            SqlParameter[] parameterArray = new SqlParameter[2];
            SqlDataReader currentReader = null;

            parameterArray[0] = new SqlParameter("@Mail", strMail);
            parameterArray[1] = new SqlParameter("@Contrasenia", strContrasenia);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Medicos_Login", parameterArray);
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

        public static int ModificarMedico(Medico unMedico)
        {
            int intRegsAffected = 0;
            SqlParameter[] parameterArray = new SqlParameter[8];


            parameterArray[0] = new SqlParameter("@IdMedico",       unMedico.IdMedico);
            parameterArray[1] = new SqlParameter("@Nombre",         unMedico.Nombre);
            parameterArray[2] = new SqlParameter("@Apellido",       unMedico.Apellido);
            parameterArray[3] = new SqlParameter("@Foto",           unMedico.Foto);
            parameterArray[4] = new SqlParameter("@IDEspecialidad", unMedico.Especialidad);
            parameterArray[5] = new SqlParameter("@Mail",           unMedico.Mail);
            parameterArray[6] = new SqlParameter("@Contrasenia",    unMedico.Contrasenia);
            parameterArray[7] = new SqlParameter("@DNI",            unMedico.Dni);

            try
            {
                intRegsAffected = DatabaseHelper.ExecuteNonQuery("Medicos_ModificarMedico", parameterArray);
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }
            return intRegsAffected;
        }

        public static List<SelectListItem> ObtenerTodosDropDown()
        {
            List<SelectListItem> listaDevolver = new List<SelectListItem>();
            SelectListItem elemento = null;

            SqlDataReader currentReader = null;

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Medicos_ObtenerTodos");
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
        
        
        public static int AgregarMedico(Medico unMedico)
        {
            int intNewId = 0;
            SqlParameter[] parameterArray = new SqlParameter[7];

            parameterArray[0]       = new SqlParameter("@Nombre",               unMedico.Nombre);
            parameterArray[1]       = new SqlParameter("@Apellido",             unMedico.Apellido);
            parameterArray[2]       = new SqlParameter("@Foto",                 unMedico.Foto);
            parameterArray[3]       = new SqlParameter("@IDEspecialidad",       unMedico.Especialidad);
            parameterArray[4]       = new SqlParameter("@Mail",                 unMedico.Mail);
            parameterArray[5]       = new SqlParameter("@Contrasenia",          unMedico.Contrasenia);
            parameterArray[6]       = new SqlParameter("@DNI",                  unMedico.Dni);

            try
            {
                intNewId = Convert.ToInt32(DatabaseHelper.ExecuteScalar("Medicos_AgregarMedico", parameterArray));
            }
            catch (Exception ex)
            {
                CustomLog.LogException(ex);
            }
            return intNewId;
        }

        public static Medico ObtenerPorId(int intID)
        {
            Medico returnEntity = null;
            SqlParameter[] parameterArray = new SqlParameter[1];
            SqlDataReader currentReader = null;

            parameterArray[0] = new SqlParameter("@intID", intID);

            try
            {
                currentReader = DatabaseHelper.ExecuteReader("Medicos_ObtenerPorId", parameterArray);
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

        private static Medico DataReaderToObject(SqlDataReader currentReader)
        {
            Medico returnEntity = null;

            if (currentReader != null)
            {
                returnEntity = new Medico();

                returnEntity.IdMedico       = (currentReader["Id"]                  != DBNull.Value ? (int)currentReader["Id"] : 0);
                returnEntity.Nombre         = (currentReader["Nombre"]              != DBNull.Value ? (string)currentReader["Nombre"] : "");
                returnEntity.Apellido       = (currentReader["Apellido"]            != DBNull.Value ? (string)currentReader["Apellido"] : "");
                returnEntity.Foto           = (currentReader["Foto"]                != DBNull.Value ? (string)currentReader["Foto"] : "");
                returnEntity.Especialidad   = (currentReader["IDEspecialidad"]      != DBNull.Value ? (int)currentReader["IDEspecialidad"] : 0);
                returnEntity.Mail           = (currentReader["Mail"]                != DBNull.Value ? (string)(currentReader["Mail"]) : "");
                returnEntity.Contrasenia    = (currentReader["Contrasenia"]         != DBNull.Value ? (string)currentReader["Contrasenia"] : "");
                returnEntity.Dni            = (currentReader["DNI"]                 != DBNull.Value ? (int)currentReader["DNI"] : 0);
            }

            return returnEntity;
        }

        
    }
}