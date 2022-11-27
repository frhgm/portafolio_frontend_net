using classLibrary.DTOs;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace classLibrary.DataServices
{
    public class SolicitudesDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Instancia el data service con la direccion base (para no repetirla en cada llamada)
        /// </summary>
        public SolicitudesDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress =
                "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }


        
        public async Task<Solicitudes_Pedido> TraerSolicitudes()
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_all_solicitud_pedido/",
                        new { });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != string.Empty)
                    {
                        var solicitudes =
                            JsonSerializer.Deserialize<Solicitudes_Pedido>(content, _jsonSerializerOptions);
                        return solicitudes;
                    }
                }
                else
                {
                    Debug.WriteLine("---> No es una respuesta del rango 200");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            return null;
        }
        
        public async Task<SolicitudesPedidos_Recibidas> TraerSolicitudesRecibidas()
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_all_solicitudes_pedido_recibidas/",
                        new { });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != string.Empty)
                    {
                        var solicitudes =
                            JsonSerializer.Deserialize<SolicitudesPedidos_Recibidas>(content, _jsonSerializerOptions);
                        return solicitudes;
                    }
                }
                else
                {
                    Debug.WriteLine("---> No es una respuesta del rango 200");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            return null;
        }
        /// <summary>
        /// Trae los detalles de una solicitud en particular
        /// </summary>
        /// <param name="idSolicitudPedido">Id de solicitud seleccionada</param>
        /// <returns>Devuelve todos los detalles de una solicitud</returns>
        public async Task<ListaDetallesSolicitudPedido> TraerDetallesSolcitudPedido(int  idSolicitudPedido)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_detalle_solicitud_pedido/",
                        new { in_id_solicitud_pedido = idSolicitudPedido.ToString() });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(); 
                    if (content != string.Empty)
                    {
                        var detalles =
                            JsonSerializer.Deserialize<ListaDetallesSolicitudPedido>(content, _jsonSerializerOptions);
                        return detalles;
                    }
                }
                else
                {
                    Debug.WriteLine("---> No es una respuesta del rango 200");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            return null;
        }


        /// <summary>
        /// Crea una solicitud con los datos recibidos de formulario
        /// </summary>
        /// <param name="solicitudPedido">Una solicitud completa, donde se procesaran toda su informacion</param>
        /// <returns>La nueva solicitud realizada, o null si fallo</returns>
        public async Task<bool> CrearSolicitud(SolicitudPedido solicitudPedido)
        {
            try
            {
                object json = new { solicitud = solicitudPedido };
                // string jsonSolicitud =
                    // JsonSerializer.Serialize<object>(new {in_objeto_json = json}, _jsonSerializerOptions);
                
                string jsonSolicitud = JsonConvert.SerializeObject(json);
                jsonSolicitud = jsonSolicitud.Replace("\"", "\\\"");

                StringContent content = new StringContent(jsonSolicitud, Encoding.UTF8, "application/json");
                Debug.Write(content.ReadAsStringAsync());
                HttpResponseMessage response =
                    await _httpClient.PostAsync($"{_baseAddress}sp_insert_solicitud_y_detalle/", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Solicitud ingresada!");
                    return true;
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hubo un error {ex.Message}");
            }

            return false;
        }
    }
}