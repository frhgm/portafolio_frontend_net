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
        
        [JsonPropertyName("detalle_productos")]
        public List<DetalleProducto> DetalleProductos { get; set; }

        public SolicitudPedido()
        {
        }

        public SolicitudPedido(string usuario_id,  string direccion,  List<DetalleProducto> detalleProductos)
        {
            Rut = usuario_id;
            Direccion = direccion;
            DetalleProductos = detalleProductos;
        }
    }
}