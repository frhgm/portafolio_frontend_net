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

namespace app.Data.Implementations
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
        public async Task<SolicitudesPedidos> TraerSolicitudes()
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
                            JsonSerializer.Deserialize<SolicitudesPedidos>(content, _jsonSerializerOptions);
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
        /// Apartando el RUT, permitira actualizar un usuario
        /// </summary>
        /// <param name="usuario">Un usuario completo, para identificar y capturar campos a modificar</param>
        /// <returns>El nuevo usuario</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ActualizarUsuario(ActualizarUsuario usuario)
        {
            try
            {
                string jsonUsuario = JsonSerializer.Serialize(usuario, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await _httpClient.PostAsync($"{_baseAddress}sp_update_usuario/", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Usuario creado!");
                }
                else
                {
                    Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hubo un error {ex.Message}");
            }
        }

        /// <summary>
        /// Borra un usuario por el RUT
        /// </summary>
        /// <param name="rut">RUT recibido desde formulario</param>
        /// <returns>Nada</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task BorrarUsuario(Usuario usuario)
        {
            BorrarUsuario aBorrar = new BorrarUsuario();
            aBorrar.Rut = usuario.Rut;
            try
            {
                string jsonRut = JsonSerializer.Serialize<BorrarUsuario>(aBorrar, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonRut, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_baseAddress}sp_delete_usuario/"),
                    Content = content
                };
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Usuario creado!");
                }
                else
                {
                    Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hubo un error {ex.Message}");
            }
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