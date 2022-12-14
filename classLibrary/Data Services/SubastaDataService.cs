using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using classLibrary.DTOs;

namespace classLibrary.DataServices
{
    public class SubastaDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Instancia el data service con la direccion base (para no repetirla en cada llamada)
        /// </summary>
        public SubastaDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress =
                "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }

        public async Task<ListaSubastas> TraerSubastas()
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_all_subastas/",
                        new { });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(); //tambien

                    if (content != string.Empty)
                    {
                        var subastas =
                            JsonSerializer.Deserialize<ListaSubastas>(content, _jsonSerializerOptions);
                        return subastas;
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

        public async Task<bool> PuedeInsertarSubasta(int pedidoId)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_insert_subasta_checker/",
                        new { in_pedido_id = pedidoId.ToString() });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != string.Empty)
                    {
                        var result =
                            JsonSerializer.Deserialize<PedidoAsociado>(content, _jsonSerializerOptions);
                        if (result.pedidosAsociados.Count == 0)
                        {
                            return true;
                        }

                        return false;
                    }
                }

                return true;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hubo un error {ex.Message}");
            }

            return false;
        }

        public async Task<bool> CrearSubasta(CrearSubasta crearSubasta)
        {
            try
            {
                if (!await PuedeInsertarSubasta(crearSubasta.PedidoId))
                {
                    MessageBox.Show("Ya existe una subasta asociada a este pedido");
                    return false;
                }

                string jsonSubasta = JsonSerializer.Serialize<CrearSubasta>(crearSubasta, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonSubasta, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await _httpClient.PostAsync($"{_baseAddress}sp_insert_subasta/", content);

                var result = response.Content.ReadAsStringAsync().Result;
                var responseAPI = JsonSerializer.Deserialize<ResponseGeneral>(result, _jsonSerializerOptions);
                if (responseAPI.MensajeSalida.Contains("CORRECTAMENTE"))
                {
                    return true;
                }

                Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
                MessageBox.Show("No se ingreso subasta, vuelva a intentar");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hubo un error {ex.Message}");
                MessageBox.Show("No se ingreso subasta, vuelva a intentar");
                return false;
            }
        }
    }
}