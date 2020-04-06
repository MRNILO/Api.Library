using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Library.Services
{
    using System.Data;
    using System.Data.SqlClient;
    using Api.Library.Models;
    using Api.Library.Interfaces;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Api.Library.Helpers.Datos;

    public class LoginService : ILogin, IDisposable
    {
        #region Constructor y Variables
        SqlConexion sql = null;
        ConnectionType type = ConnectionType.NONE;

        LoginService() { }
        public static LoginService CrearInstanciaSQL(SqlConexion sql)
        {
            LoginService log = new LoginService()
            {
                sql = sql,
                type = ConnectionType.MSSQL
            };

            return log;
        }
        #endregion

        public User EstablecerLogin(string nick, string password)
        {
            throw new Exception();
        }

        public List<User> ObtenerUsers()
        {
            List<User> list = new List<User>();
            try
            {
                sql.PrepararProcedimiento("dbo.[USER.GetAllJSON]", new List<SqlParameter>());
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Usuario"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject item in arr.Children<JObject>())
                            {
                                list.Add(new User()
                                {
                                    ID = Convert.ToInt32(item["Id"].ToString()),
                                    Name = item["Name"].ToString(),
                                    CreatedDate = DateTime.Parse(item["CreateDate"].ToString())
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return list;
        }

        #region IDisposable
        bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (sql != null)
                    {
                        sql.Desconectar();
                        sql.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
