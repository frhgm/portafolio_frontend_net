using classLibrary.DTOs;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace classLibrary.DataServices
{
    public class UsuariosDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Instancia el data service con la direccion base (para no repetirla en cada llamada)
        /// </summary>
        public UsuariosDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress =
                "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }

        //public async void PostGeneral(object generico, string url, Usuario usuario)
        //{
        //    string message = "";
        //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, generico);

        //}
        /// <summary>
        /// Envia las credenciales de usuario a la API, si recibe algo, entonces el usuario existe
        /// </summary>
        /// <param name="rut">Es el rut ingresado al formulario por el usuario</param>
        /// <param name="pass">Es la contrasena ingresada al formulario por el usuario</param>
        /// <returns>Un usuario, o null</returns>
        public async Task<Usuario> Login(string rut, string pass)
        {
            string message = "";
            object userParams = new { p_rut = rut, p_pass = pass };
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_login/", userParams);
                var x = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    message = await response.Content.ReadAsStringAsync();
                    if (message != null)
                    {
                        var usuarios = JsonSerializer.Deserialize<ListaUsuario>(message);
                        if (usuarios is null || usuarios.usuario.Count == 0)
                        {
                            return null;
                        }
                        else
                        {
                            return usuarios.usuario[0];
                            //this.Content = null;
                            //Dashboard db = new();
                            //db.Show();
                            //db.Show();
                        }
                    }

                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo por tal y tal", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Ejecuta sp_get_all_users para recuperar los usuarios
        /// </summary>
        /// <returns>Devuelve una lista de usuarios registrados en el sistema</returns>
        public async Task<ListaUsuarios> TraerUsuarios()
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_all_users/", new { });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != string.Empty)
                    {
                        var usuarios = JsonSerializer.Deserialize<ListaUsuarios>(content, _jsonSerializerOptions);
                        return usuarios;
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
        public async Task<ActualizarUsuario> ActualizarUsuario(ActualizarUsuario usuario)
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
                    return usuario;
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
                return null;
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
        /// Crea un usuario con los datos recibidos de formulario
        /// </summary>
        /// <param name="usuario">Un usuario completo, donde se procesaran todos sus datos</param>
        /// <returns>El nuevo usuario creado, o null si fallo</returns>
        public async Task<RegistrarUsuario> CrearUsuario(RegistrarUsuario usuario)
        {
            try
            {
                string jsonUsuario = JsonSerializer.Serialize<RegistrarUsuario>(usuario, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await _httpClient.PostAsync($"{_baseAddress}sp_insert_usuario/", content);
                var result = response.Content.ReadAsStringAsync().Result;
                var responseAPI = JsonSerializer.Deserialize<ResponseGeneral>(result, _jsonSerializerOptions);
                if (responseAPI.MensajeSalida.Contains("CORRECTAMENTE"))
                {
                    /*
                     TODO Esto facilmente podria extraerse en otro metodo, y si no se creo el contrato se puede
                      controlar si se borra el usaurio recien creado, o como minimo limpiar el codigo.
                    */
                    jsonUsuario =
                        JsonSerializer.Serialize<object>(new { in_rut = usuario.Rut }, _jsonSerializerOptions);
                    content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync($"{_baseAddress}sp_insert_contrato/", content);

                    result = response.Content.ReadAsStringAsync().Result;
                    responseAPI = JsonSerializer.Deserialize<ResponseGeneral>(result, _jsonSerializerOptions);
                    if (responseAPI.MensajeSalida.Contains("CORRECTAMENTE"))
                    {
                        return usuario;
                    }
                    else
                    {
                        // TODO Se debera borrar usuario si no se registro un contrato
                    }
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