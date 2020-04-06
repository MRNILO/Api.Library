using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Api.Library.Helpers.Datos;
using Api.Library.Interfaces;
using Api.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoreApiAzure.Controllers
{
    [Route("api/control")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IConfiguration _configuration;
        string ConnectionStringAzure;
        string ConnectionStringLocal;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                ConnectionStringAzure = _configuration.GetConnectionString("ConnectionStringAzure");
            else
                ConnectionStringLocal = _configuration.GetValue<string>("ConnectionStringLocal");
        }

        public ActionResult<List<User>> GetAllUsers()
        {
            using (ILogin Login = Factorizador.CrearConexionServicio(Api.Library.Models.ConnectionType.MSSQL, ConnectionStringAzure))
            {
                List<Api.Library.Models.User> objusrs = Login.ObtenerUsers();
                return objusrs;
            }
        }
    }
}