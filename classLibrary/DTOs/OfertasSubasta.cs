using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace classLibrary.DTOs
{
    public class CrearOferta
    {
        [JsonPropertyName("in_usuario_id")]
        public string Rut { get; set; }

        [JsonPropertyName("in_subasta_id")]
        public int SubastaId { get; set; }

        [JsonPropertyName("in_valor_ofertado")]
        public int ValorOfertado { get; set; }
    }

    public class OfertasSubastas
    {
        [JsonPropertyName("oferta_subasta_id")]
        public int OfertaSubastaId { get; set; }

        [JsonPropertyName("subasta_id")] public int SubastaId { get; set; }

        [JsonPropertyName("monto_ofertado")] public int MontoOfertado { get; set; }

        [JsonPropertyName("precio_piso")] public int PrecioPiso { get; set; }

        [JsonPropertyName("fecha_oferta")] public DateTime FechaOferta { get; set; }

        [JsonPropertyName("fecha_inicio_subasta")]
        public DateTime FechaInicioSubasta { get; set; }
    }

    public class ListaOfertasSubasta
    {
        [JsonPropertyName("ofertas_subasta")] public List<OfertasSubastas> OfertasSubasta { get; set; }
    }
}