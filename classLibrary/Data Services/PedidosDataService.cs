using classLibrary.DTO;
using classLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
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
    public class PedidosDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Instancia el data service con la direccion base (para no repetirla en cada llamada)
        /// </summary>
        public PedidosDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress =
                "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }


        public async Task<ProductoProductor> TraerProductosProductorPedido(int idProductoCliente)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_producto_productor_pedido/",
                        new { in_id_pc = idProductoCliente.ToString() }); //puedo recibir

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(); //tambien

                    if (content != string.Empty)
                    {
                        var productos =
                            JsonSerializer.Deserialize<ListaProductoProductor>(content, _jsonSerializerOptions);
                        return productos.productoProductor[0];
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

        public async Task<FilaProductoProductor> PoblarDetallePedido(int productoProductorId, int productoClienteId)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_detalle_pedido_populate/",
                        new
                        {
                            in_pp_id = productoProductorId.ToString(),
                            in_pc_id = productoClienteId.ToString()
                        }); //puedo recibir

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(); //tambien
                    if (content != string.Empty)
                    {
                        var filas =
                            JsonSerializer.Deserialize<ListaFilasProductoProductor>(content, _jsonSerializerOptions);
                        return filas.filaProductoProductor[0];
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

        public async Task<bool> CrearPedido(Pedido pedido)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // System.Net.ServicePointManager.Expect100Continue = false;
            try
            {
                CrearPedido creacion = new CrearPedido();
                creacion.pedido = pedido;

                object json = new { in_objeto_json = creacion };
                string jsonSolicitud =
                    JsonSerializer.Serialize<object>(json, _jsonSerializerOptions);

                // string jsonSolicitud = JsonConvert.SerializeObject(json);

                jsonSolicitud = jsonSolicitud.Replace("\"", "\\\"");

                StringContent content = new StringContent(jsonSolicitud, Encoding.UTF8, "application/json");
                var x = content.ReadAsStringAsync();
                
                HttpResponseMessage response =
                    await _httpClient.PostAsync($"{_baseAddress}sp_insert_pedido_y_detalle/", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Pedido ingresado!");
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