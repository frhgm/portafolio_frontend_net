using System.Text.Json.Serialization;

namespace classLibrary.DTOs
{
    public class ProductoCliente
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("producto_id")]
        public int ProductoId { get; set; }
        [JsonIgnore]
        public string? NombreProducto { get; set; }
        [JsonPropertyName("calidad")]
        public int Calidad { get; set; }
        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        
        
        public ProductoCliente(int id, int productoId, string nombreProducto, int calidad, int cantidad)
        {
            Id = id;
            ProductoId = productoId;
            NombreProducto = nombreProducto;
            Calidad = calidad;
            Cantidad = cantidad;
        }
        
    }

    public class Crear_ProductoCliente
    {
        [JsonPropertyName("producto_id")] public int ProductoId { get; set; }
        [JsonIgnore] public string? NombreProducto { get; set; }
        [JsonPropertyName("calidad")] public int Calidad { get; set; }
        [JsonPropertyName("cantidad")] public int Cantidad { get; set; }



        public Crear_ProductoCliente(int productoId, string nombreProducto, int calidad, int cantidad)
        {
            ProductoId = productoId;
            NombreProducto = nombreProducto;
            Calidad = calidad;
            Cantidad = cantidad;
        }
    }
}