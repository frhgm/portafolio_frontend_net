using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class SolicitudPedido
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public DateTime fecha { get; set; }
        public string direccion { get; set; } = "";
        public int estado_solicitud { get; set; }
    }
}
