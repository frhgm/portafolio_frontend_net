using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class Rol
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre_rol")]
        public string Nombre_Rol { get; set; }
        public Rol(int id, string nombre)
        {
            Id = id;
            Nombre_Rol = nombre;
        }

        public Rol()
        {
        }
    }
}
