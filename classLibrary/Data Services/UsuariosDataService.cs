using classLibrary.DTO;
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
using System.Threading.Tasks;
using System.Windows;

namespace app.Data.Implementations
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
            _baseAddress = "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/";
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
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_login/", userParams);

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
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_all_users/", new { });

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
        public Task ActualizarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Borra un usuario por el RUT
        /// </summary>
        /// <param name="rut">RUT recibido desde formulario</param>
        /// <returns>Nada</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task BorrarUsuario(string rut)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Crea un usuario con los datos recibidos de formulario
        /// </summary>
        /// <param name="usuario">Un usuario completo, donde se procesaran todos sus datos</param>
        /// <returns>El nuevo usuario creado, o null si fallo</returns>
        public async Task<UsuarioSalida> CrearUsuario(UsuarioSalida usuario)
        {
            try
            {
                string jsonUsuario = JsonSerializer.Serialize<UsuarioSalida>(usuario, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseAddress}sp_insert_usuario/", content);
                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Usuario creado!");
                    return usuario;
                } else
                {
                    Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
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
