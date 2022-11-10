using System.Text.Json.Serialization;

namespace classLibrary.DTOs
{
    public class DetalleProducto
    {
        [JsonPropertyName("producto_id")]
        public int ProductoId { get; set; }
        [JsonIgnore]
        public string? NombreProducto { get; set; }
        [JsonPropertyName("calidad")]
        public int Calidad { get; set; }
        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        
        
        public DetalleProducto(int productoId, string nombreProducto, int calidad, int cantidad)
        {
            ProductoId = productoId;
            NombreProducto = nombreProducto;
            Calidad = calidad;
            Cantidad = cantidad;
        }
    }
}