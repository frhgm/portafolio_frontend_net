using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classLibrary;
using classLibrary.DTO;

namespace app.Data.Interfaces
{
    internal interface IUsuariosDataService
    {
        Task<ListaUsuarios> GetAllUsuariosAsync();
        Task CrearUsuario(Usuario usuario);
        Task BorrarUsuario(string rut);
        Task ActualizarUsuario(Usuario usuario);
    }
}
