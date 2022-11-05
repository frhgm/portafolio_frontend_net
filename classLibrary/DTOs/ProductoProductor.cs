using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class ProductosProductor
    {
        [JsonPropertyName("producto_productor")]
        public List<ProductoProductor>? productosProductor { get; set; }
    }
    public class ProductoProductor
    {
        [JsonPropertyName("rut")]
        public string Rut { get; set; }
        [JsonPropertyName("id")]
        public int Id_Producto { get; set; }
        [JsonPropertyName("nombre_producto")]
        public string NombreProducto { get; set; }

        [JsonPropertyName("nombre_productor")]
        public string NombreProductor { get; set; }

        [JsonPropertyName("precio")]
        public int Precio { get; set; }

        [JsonPropertyName("calidad")]
        public int Calidad { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }


    }
}
