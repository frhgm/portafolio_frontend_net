using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace classLibrary.DTOs
{
    public class CrearPedido
    {
        [JsonPropertyName("pedido")]
        public Pedido pedido { get; set; }
    }
    public class Pedido
    {
        [JsonPropertyName("solicitud_id")]
        public string SolicitudId { get; set; }
        [JsonPropertyName("total")]
        public string Total { get; set; }
        [JsonPropertyName(("detalle_pedido"))]
        public List<CrearDetallePedido> DetallePedido { get; set; }
    }
}