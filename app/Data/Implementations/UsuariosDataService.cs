using app.Data.Interfaces;
using classLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace app.Data.Implementations
{
    internal class UsuariosDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public UsuariosDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/dev/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }

        //public async void PostGeneral(object generico, string url, Usuario usuario)
        //{
        //    string message = "";
        //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, generico);

        //}

        public async Task<Usuario> Login(string rut, string pass)
        {
            string message = "";
            object userParams = new { p_rut = rut, p_pass = pass };
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(String.Concat(_baseAddress, "sp_login/"), userParams);

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

        public async Task<ListaUsuarios> GetAllUsuariosAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(String.Concat(_baseAddress, "sp_get_all_users/"), new { });

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
        public Task ActualizarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task BorrarUsuario(string rut)
        {
            throw new NotImplementedException();
        }

        public Task CrearUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
