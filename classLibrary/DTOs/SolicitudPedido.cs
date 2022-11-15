using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class Solicitudes_Pedidos
    {
        public List<SolicitudPedido>? solicitudes_pedidos{ get; set; }
    }
    public class SolicitudPedido
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("rut_cliente")]
        public string Usuario_id { get; set; }
        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }
        [JsonPropertyName("direccion")]
        public string Direccion { get; set; } = "";
        [JsonPropertyName("estado")]
        public string Estado_solicitud { get; set; }
        public string Mostrar_Solicitud { get; set; }


        public SolicitudPedido(int id, string usuario_id, DateTime fecha, string direccion, string estado_solicitud)
        {
            Id = id;
            Usuario_id = usuario_id;
            Fecha = fecha;
            Direccion = direccion;
            Estado_solicitud = estado_solicitud;
            Mostrar_Solicitud = $"N° pedido: {Id} \n Cliente: {Usuario_id}";
        }
    }
}