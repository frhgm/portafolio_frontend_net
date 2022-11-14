using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    //TODO Falta terminar
    public class DetalleSolicitudPedido
    {
        public int Id { get; set; }
        public int ProductosCliente_Id { get; set; }
        public int SolicitudPedido_Id { get; set; }
        
    }
}