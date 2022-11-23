using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace classLibrary.DTOs
{
    public class CrearSubasta
    {
        [JsonPropertyName("in_pedido_id")] public int PedidoId { get; set; }
        [JsonPropertyName("in_precio_piso")] public int PrecioPiso { get; set; }

        public CrearSubasta(int pedidoId, int precioPiso)
        {
            PedidoId = pedidoId;
            PrecioPiso = precioPiso;
        }
    }

    
    public class PedidosAsociados
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }
    }

    public class PedidoAsociado
    {
        [JsonPropertyName("l_pedidos")]
        public List<PedidosAsociados> pedidosAsociados { get; set; }
    }


}