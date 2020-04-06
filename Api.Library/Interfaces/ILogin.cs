using System;
using System.Collections.Generic;
using System.Text;
using Api.Library.Models;

namespace Api.Library.Interfaces
{
    public interface ILogin: IDisposable
    {
        User EstablecerLogin(string nick, string password);
        List<User> ObtenerUsers();
    }
}
