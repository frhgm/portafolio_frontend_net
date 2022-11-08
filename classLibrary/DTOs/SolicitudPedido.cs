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
        // [JsonPropertyName("id")]
        // public int Id { get; set; }
        [JsonPropertyName("rut_cliente")]
        public string Rut { get; set; }
        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }
        [JsonPropertyName("direccion")]
        public string Direccion { get; set; } = "";
        [JsonPropertyName("estado")]
        public string Estado_solicitud { get; set; }
        [JsonPropertyName("detalle_productos")]
        public List<DetalleProducto> DetalleProductos { get; set; }

        public SolicitudPedido()
        {
        }

        public SolicitudPedido(string usuario_id, DateTime fecha, string direccion, string estado_solicitud)
        {
            Rut = usuario_id;
            Fecha = fecha;
            Direccion = direccion;
            Estado_solicitud = estado_solicitud;
        }
        public SolicitudPedido(string usuario_id, DateTime fecha, string direccion, string estado_solicitud, List<DetalleProducto> detalleProductos)
        {
            Rut = usuario_id;
            Fecha = fecha;
            Direccion = direccion;
            Estado_solicitud = estado_solicitud;
            DetalleProductos = detalleProductos;
        }
    }
}
