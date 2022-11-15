using classLibrary.DTO;
using classLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
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


        /// <summary>
        /// Ejecuta sp_get_all_users para recuperar los usuarios
        /// </summary>
        /// <returns>Devuelve una lista de usuarios registrados en el sistema</returns>
        public async Task<Solicitudes_Pedidos> TraerSolicitudes()
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_all_solicitud_pedido/",
                        new { }); //puedo recibir

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(); //tambien
                    if (content != string.Empty)
                    {
                        var solicitudes =
                            JsonSerializer.Deserialize<Solicitudes_Pedidos>(content, _jsonSerializerOptions);
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
                        new { in_id_solicitud_pedido = idSolicitudPedido.ToString() }); //puedo recibir

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(); //tambien
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