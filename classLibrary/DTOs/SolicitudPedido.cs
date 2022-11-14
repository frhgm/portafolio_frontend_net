using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class SolicitudesPedidos
    {
        public List<SolicitudPedido>? solicitudes_pedidos{ get; set; }
    }
    public class SolicitudPedido
    {
        [JsonPropertyName("usuario_id")]
        public string Rut { get; set; }
        
        [JsonPropertyName("direccion")]
        public string Direccion { get; set; } = "";
        [JsonPropertyName("estado")]
        public string Estado_solicitud { get; set; }

        public string Mostrar_Solicitud { get; set; }

        public SolicitudPedido(int id, string usuario_id, DateTime fecha, string direccion, string estado_solicitud)
        {
            Rut = usuario_id;
            Direccion = direccion;
            Estado_solicitud = estado_solicitud;
            Mostrar_Solicitud = $"Solicitud N°: {Id}\nCliente:{Usuario_id}";
        }
    }
}