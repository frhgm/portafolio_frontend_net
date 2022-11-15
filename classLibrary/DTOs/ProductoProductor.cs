using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace classLibrary.DTOs
{
    public class ListaProductoProductor
    {
        [JsonPropertyName("producto_productor")]
        public List<ProductoProductor> productoProductor { get; set; }
    }
    public class ProductoProductor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("producto_id")] public int ProductoId { get; set; }
        [JsonPropertyName("precio")] public int Precio { get; set; }
        [JsonIgnore] public string? NombreProducto { get; set; }
        [JsonPropertyName("calidad")] public int Calidad { get; set; }
        [JsonPropertyName("cantidad")] public int Cantidad { get; set; }
        [JsonPropertyName("productor_rut")] public string ProductorRut { get; set; }



        public ProductoProductor(int id, int productoId, int precio, string? nombreProducto, int calidad, int cantidad, string productorRut)
        {
            Id = id;
            ProductoId = productoId;
            Precio = precio;
            NombreProducto = nombreProducto;
            Calidad = calidad;
            Cantidad = cantidad;
            ProductorRut = productorRut;
        }
    }
}