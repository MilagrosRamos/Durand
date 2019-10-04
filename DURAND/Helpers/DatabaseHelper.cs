using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DURAND.Helpers {
    /// <summary>
    ///     Clase que agrupa funcionalidades de acceso a Base de Datos SQL Server.
    /// </summary>
    public class DatabaseHelper {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection() {
            return GetConnection(false);
        }

        /// <summary>
        ///     Retorna la conexion a la base de datos y procede o no a abrirla.
        ///     
        ///     <example>Por ejemplo:
        ///     <code>
        ///        SqlConnection cnn = DatabaseHelper.GetConnection(false);
        ///        cnn.open();
        ///     </code>
        ///     como resultado en <c>cnn</c> se obtiene la conexcion de la base de datos.
        /// </example>
        /// </summary>
        /// <param name="blnOpen">insica si hay que abrir la conexion.</param>
        /// <returns></returns>
        public static SqlConnection GetConnection(bool blnOpen) {
            string          strConnectionString;
            SqlConnection   returnConnection;

            strConnectionString = AppSettingsHelper.GetAppSetting("DatabaseConnectionString", "");

            returnConnection = new SqlConnection(strConnectionString);
            if (blnOpen) {
                TryOpenConnection(returnConnection);
            }

            return returnConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentConnection"></param>
        /// <returns></returns>
        public static bool TryOpenConnection(SqlConnection currentConnection) {
            bool blnReturnValue = false;

            if (currentConnection != null) {
                try {
                    currentConnection.Open();
                    blnReturnValue = true;
                } catch (Exception ex) {
                    throw;
                }
            }
            return blnReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentConnection"></param>
        /// <returns></returns>
        public static bool CloseConnection(SqlConnection currentConnection) {
            bool blnReturnValue = false;

            if (currentConnection != null) {
                if(currentConnection.State != ConnectionState.Closed) {
                    currentConnection.Close();
                    blnReturnValue = true;
                }
            }

            return blnReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentReader"></param>
        public static void CloseAndDisposeReader(ref SqlDataReader currentReader) {
            if (currentReader != null) {
                if (!currentReader.IsClosed) {
                    currentReader.Close();
                }
                currentReader.Dispose();
                currentReader = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string storedProcedureName) {
            return ExecuteNonQuery(storedProcedureName, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterArray"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameterArray) {
            SqlConnection   connection;
            SqlCommand      command;
            int             intReturnValue = 0; 

            connection          = GetConnection(true);
            command             = new SqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameterArray != null) {
                foreach (SqlParameter item in parameterArray) {
                    command.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
            }

            try {
                intReturnValue = command.ExecuteNonQuery();
            } catch (Exception ex) {
                throw;
            }
            command.Dispose();
            CloseConnection(connection);

            return intReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string storedProcedureName) {
            return ExecuteScalar(storedProcedureName, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterArray"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string storedProcedureName, SqlParameter[] parameterArray) {
            SqlConnection   connection;
            SqlCommand      command;
            object          oReturnValue = null; 

            connection          = GetConnection(true);
            command             = new SqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameterArray != null) {
                foreach (SqlParameter item in parameterArray) {
                    command.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
            }

            try {
                oReturnValue = (object)command.ExecuteScalar();
            } catch (Exception ex) {
                throw;
            }
            command.Dispose();
            CloseConnection(connection);

            return oReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string storedProcedureName) {
            return ExecuteReader(storedProcedureName, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterArray"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string storedProcedureName, SqlParameter[] parameterArray) {
            SqlConnection   connection;
            SqlCommand      command;
            SqlDataReader   returnReader = null;

            connection          = GetConnection(true);
            command             = new SqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameterArray != null) {
                foreach (SqlParameter item in parameterArray) {
                    command.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
            }

            try {
                returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            } catch (Exception ex) {
                throw;
            }

            return returnReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static object ToDBNull(string strValue) {
            object returnObject = DBNull.Value;
            if (!string.IsNullOrEmpty(strValue)) {
                returnObject = strValue;
            }
            return returnObject;
        }
    }
}
