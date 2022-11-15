using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace classLibrary.DTOs
{
    public class DetallePedido
    {
        
    }

    public class ListaFilasProductoProductor
    {
        [JsonPropertyName("fila_producto_productor")]
        public List<FilaProductoProductor> filaProductoProductor { get; set; }
    }
    public class FilaProductoProductor
    {
        [JsonPropertyName("producto_productor_id")]
        public int ProductoProductorId { get; set; }
        [JsonPropertyName("productor_rut")]
        public string ProductorRut { get; set; }
        [JsonPropertyName("nombre_producto")]
        public string NombreProducto { get; set; }
        [JsonPropertyName("calidad")]
        public int Calidad { get; set; }
        [JsonPropertyName("cantidad_producto_cliente")]
        public int CantidadProductoCliente { get; set; }
         [JsonPropertyName("precio")]
        public int Precio { get; set; }
         [JsonPropertyName("ganancia")]
        public int Ganancia { get; set; }
         [JsonPropertyName("total")]
        public int Total { get; set; }
        
    }
}