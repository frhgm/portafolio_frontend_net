using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace classLibrary.DTOs
{
    // public class CrearPedido
    // {
    //     [JsonPropertyName("pedido")]
    //     public Pedido pedido { get; set; }
    // }
    public class Pedido
    {
        public CrearPedido pedido { get; set; }
    }
    public class CrearPedido
    {
        [JsonPropertyName("solicitud_id")] public int SolicitudId { get; set; }
        // [JsonPropertyName("total")] public int Total { get; set; }
        [JsonPropertyName(("detalle_pedido"))] public List<CrearDetallePedido> DetallePedido { get; set; }
    }

    public class Pedidos
    {
        [JsonPropertyName("pedido_id")] public int PedidoId { get; set; }

        [JsonPropertyName("oferta_subasta_id")]
        public object OfertaSubastaId { get; set; }

        [JsonPropertyName("fecha_pedido")] public DateTime FechaPedido { get; set; }

        [JsonPropertyName("estado_pedido_id")] public int EstadoPedidoId { get; set; }

        [JsonPropertyName("estado_pedido")] public string EstadoPedido { get; set; }

        [JsonPropertyName("total")] public int Total { get; set; }

        [JsonPropertyName("solicitud_id")] public int SolicitudId { get; set; }

        [JsonPropertyName("fecha_solicitud")] public DateTime FechaSolicitud { get; set; }

        [JsonPropertyName("direccion")] public string Direccion { get; set; }

        [JsonPropertyName("nota")] public object Nota { get; set; }

        [JsonPropertyName("usuario_id")] public string UsuarioId { get; set; }
        public string Mostrar_Pedido { get; set; }

        public Pedidos(int pedidoId, object ofertaSubastaId, DateTime fechaPedido, int estadoPedidoId,
            string estadoPedido, int total, int solicitudId, DateTime fechaSolicitud, string direccion, object nota,
            string usuarioId)
        {
            PedidoId = pedidoId;
            OfertaSubastaId = ofertaSubastaId;
            FechaPedido = fechaPedido;
            EstadoPedidoId = estadoPedidoId;
            EstadoPedido = estadoPedido;
            Total = total;
            SolicitudId = solicitudId;
            FechaSolicitud = fechaSolicitud;
            Direccion = direccion;
            Nota = nota;
            UsuarioId = usuarioId;
            Mostrar_Pedido = $"Pedido N°: {PedidoId} \nCliente: {UsuarioId}";
        }
    }

    public class ListaPedidos
    {
        public List<Pedidos> pedidos { get; set; }
    }
}