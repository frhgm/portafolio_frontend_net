using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class Productos
    {
        public List<Producto>? productos { get; set; }
    }
    public class Producto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre_producto")]
        public string NombreProducto { get; set; }
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }
        [JsonPropertyName("imagen")]
        public string Imagen { get; set; }


        // No tiene un constructor vacio debido a que son todos los campos obligatorios
        public Producto(int id, string nombreProducto, string descripcion, string imagen)
        {
            Id = id;
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            Imagen = imagen;
        }
    }

    public class RegistrarProducto
    {
        [JsonPropertyName("in_nombre")]
        public string NombreProducto { get; set; }
        [JsonPropertyName("in_descripcion")]
        public string Descripcion { get; set; }
        [JsonPropertyName("in_imagen")]
        public string Imagen { get; set; }


        public RegistrarProducto(string nombreProducto, string descripcion, string imagen)
        {
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            Imagen = imagen;
        }
    }

    public class BorrarProducto
    {
        public string out_glosa { get; set; }
        public string out_mensaje_salida { get; set; }
        public string out_estado { get; set; }
    }

}
