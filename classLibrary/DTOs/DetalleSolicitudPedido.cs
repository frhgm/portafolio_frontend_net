using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class ListaDetallesSolicitudPedido
    {
        [JsonPropertyName("detalles_solicitud_pedido")]
        public List<DetalleSolicitudPedido> DetallesSolicitudPedido { get; set; }
    }
    //TODO Falta terminar
    public class DetalleSolicitudPedido
    {
        [JsonPropertyName("producto_cliente_id")]
        public int ProductoClienteId { get; set; }
        [JsonPropertyName("solicitud_pedido_id")]
        public int SolicitudPedidoId { get; set; }
        [JsonPropertyName("producto_id")]
        public int ProductoId { get; set; }
        [JsonPropertyName("calidad")]
        public int Calidad { get; set; }
        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }
        [JsonPropertyName("nombre_producto")]
        public string NombreProducto { get; set; }
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }
        [JsonPropertyName("imagen")]
        public string Imagen { get; set; }
        
    }
}