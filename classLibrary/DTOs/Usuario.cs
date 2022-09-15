using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary
{
    public class ListaUsuarios
    {
        public List<Usuario>? usuarios { get; set; }
    }

    public class ListaUsuario
    {
        public List<Usuario>? usuario { get; set; }
    }
    public class Usuario
    {
        [JsonPropertyName("rut")]
        public string Rut { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("apellido_materno")]
        public string ApellidoMaterno { get; set; }

        [JsonPropertyName("apellido_paterno")]
        public string ApellidoPaterno { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefono")]
        public long Telefono { get; set; }

        [JsonPropertyName("rol_id")]
        public int RolId { get; set; }

        [JsonPropertyName("nombre_rol")]
        public string NombreRol { get; set; }

        [JsonPropertyName("fecha_creacion")]
        public DateTimeOffset? FechaCreacion { get; set; }

        public Usuario(string rut, string nombre, string apellidoMaterno, string apellidoPaterno, string email, long telefono, int rolId, string nombreRol, DateTimeOffset? fechaCreacion)
        {
            Rut = rut;
            Nombre = nombre;
            ApellidoMaterno = apellidoMaterno;
            ApellidoPaterno = apellidoPaterno;
            Email = email;
            Telefono = telefono;
            RolId = rolId;
            NombreRol = nombreRol;
            FechaCreacion = fechaCreacion;
        }
    }
}
