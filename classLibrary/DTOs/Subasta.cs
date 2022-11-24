using System;
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
        [JsonPropertyName("pedido_id")] public int PedidoId { get; set; }
    }

    public class PedidoAsociado
    {
        [JsonPropertyName("l_pedidos")] public List<PedidosAsociados> pedidosAsociados { get; set; }
    }

    public class ListaSubastas
    {
        public List<Subasta> subastas { get; set; }
    }

    public class Subasta
    {
        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("pedido_id")] public int PedidoId { get; set; }

        [JsonPropertyName("fecha")] public DateTime Fecha { get; set; }

        [JsonPropertyName("precio_piso")] public int PrecioPiso { get; set; }

        [JsonPropertyName("oferta_subasta_id")]
        public object OfertaSubastaId { get; set; }

        [JsonPropertyName("fecha_pedido")] public DateTime FechaPedido { get; set; }

        [JsonPropertyName("estado_pedido_id")] public int EstadoPedidoId { get; set; }

        [JsonPropertyName("estado_pedido")] public string EstadoPedido { get; set; }

        [JsonPropertyName("total")] public int Total { get; set; }
    }
}