using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Library.Interfaces
{
    using Services;
    using Helpers.Datos;

    public static class Factorizador
    {
        public static ILogin CrearConexionServicio(Models.ConnectionType type, string connectionString)
        {
            ILogin nuevoMotor = null;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = LoginService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:
                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }
    }
}
