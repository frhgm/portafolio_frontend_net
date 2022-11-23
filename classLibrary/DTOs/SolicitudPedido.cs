using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class SolicitudesPedidos_Recibidas
    {
        public List<SolicitudPedido_Recibida> solicitudes_pedido_recibidas { get; set; }
    }

    public class Solicitudes_Pedido
    {
        public List<SolicitudPedido>? solicitudes_pedidos { get; set; }
    }

    public class SolicitudPedido
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("rut_cliente")] public string Usuario_id { get; set; }
        [JsonPropertyName("fecha")] public DateTime Fecha { get; set; }
        [JsonPropertyName("direccion")] public string Direccion { get; set; } = "";
        [JsonPropertyName("estado")] public string Estado_solicitud { get; set; }


        public SolicitudPedido(int id, string usuario_id, DateTime fecha, string direccion, string estado_solicitud)
        {
            Id = id;
            Usuario_id = usuario_id;
            Fecha = fecha;
            Direccion = direccion;
            Estado_solicitud = estado_solicitud;
        }
    }

    public class SolicitudPedido_Recibida
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("rut_cliente")] public string Usuario_id { get; set; }
        public string Mostrar_Solicitud { get; set; }

        public SolicitudPedido_Recibida(int id, string usuario_id)
        {
            Id = id;
            Usuario_id = usuario_id;
            Mostrar_Solicitud = $"Solicitud N°: {Id} \nCliente: {Usuario_id}";
        }
    }
}