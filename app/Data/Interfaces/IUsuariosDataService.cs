using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.Usuarios;
using classLibrary;
namespace app.Data.Interfaces
{
    internal interface IUsuariosDataService
    {
        Task<ListarUsuarios> GetAllUsuariosAsync();
        Task CrearUsuario(Usuario usuario);
        Task BorrarUsuario(string rut);
        Task ActualizarUsuario(Usuario usuario);
    }
}
