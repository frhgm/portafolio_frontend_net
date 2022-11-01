using classLibrary.DTO;
using classLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace classLibrary.DataServices
{
    public class ProductosDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Instancia el data service con la direccion base (para no repetirla en cada llamada)
        /// </summary>
        public ProductosDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }


        /// <summary>
        /// Ejecuta sp_get_all_productos para recuperar los productos
        /// </summary>
        /// <returns>Devuelve una lista de productos en el sistema</returns>

        public async Task<Productos> TraerProductos()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_all_productos/", new { }); //puedo recibir

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(); //tambien
                    if (content != string.Empty)
                    {
                        var solicitudes = JsonSerializer.Deserialize<Productos>(content, _jsonSerializerOptions);
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

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseAddress}sp_update_usuario/", content);
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
        /// <param name="in_id">Id de producto seleccionado a borrar/param>
        /// <returns>Nada</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> BorrarProducto(int in_id)
        {
            try
            {
                string json = JsonSerializer.Serialize<object>(new { in_id }, _jsonSerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_baseAddress}sp_delete_producto/"),
                    Content = content
                };
                var response = await _httpClient.SendAsync(request);

                var result = response.Content.ReadAsStringAsync().Result;
                var responseAPI = JsonSerializer.Deserialize<BorrarProducto>(result, _jsonSerializerOptions);
                if (responseAPI.out_mensaje_salida.Contains("ELIMINADO"))
                {
                    Debug.WriteLine("Producto eliminado!");
                    return true;
                }
                else
                {
                    Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hubo un error {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Crea un usuario con los datos recibidos de formulario
        /// </summary>
        /// <param name="producto">Un producto completo, donde se procesara e intentara insertar todos sus datos</param>
        /// <returns>El mismo usuario enviado en caso de exito, o null si no</returns>
        public async Task<RegistrarProducto> CrearProducto(RegistrarProducto producto)
        {
            try
            {
                string jsonProducto = JsonSerializer.Serialize<RegistrarProducto>(producto, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonProducto, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseAddress}sp_insert_producto/", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Producto creado!");
                    return producto;
                }
                else
                {
                    Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Hubo un error {ex.Message}");
            }
            return null;
        }
    }
}
