using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class ResponseGeneral
    {
        [JsonPropertyName("out_glosa")]
        public string? Glosa { get; set; }
        [JsonPropertyName("out_mensaje_salida")]
        public string? MensajeSalida { get; set; }
        [JsonPropertyName("out_estado")]
        public int? Estado { get; set; }
    }
}
