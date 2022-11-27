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
    public class OfertasSubastaDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Instancia el data service con la direccion base (para no repetirla en cada llamada)
        /// </summary>
        public OfertasSubastaDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress =
                "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }

        /// <summary>
        /// A diferencia de otros metodos "Traer", este trae solo las ofertas asociadas a una subasta en particular
        /// </summary>
        /// <param name="subastaId">ID de subasta seleccionada en pantalla anterior</param>
        /// <returns>Lista filtrada de ofertas para una subasta</returns>
        public async Task<ListaOfertasSubasta> TraerOfertasDeSubasta(int subastaId)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync($"{_baseAddress}sp_get_ofertas_de_subasta/",
                        new { in_subasta_id = subastaId });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    if (content != string.Empty)
                    {
                        var ofertas =
                            JsonSerializer.Deserialize<ListaOfertasSubasta>(content, _jsonSerializerOptions);
                        return ofertas;
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

        public async Task<int> Subastar(int pedidoId)
        {
            try
            {
                object pedido = new { in_pedido = pedidoId };
                string jsonSubastador = JsonSerializer.Serialize<object>(pedido, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonSubastador, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await _httpClient.PostAsync($"{_baseAddress}sp_subastador/", content);

                var result = response.Content.ReadAsStringAsync().Result;
                var responseAPI = JsonSerializer.Deserialize<ResponseGeneral>(result, _jsonSerializerOptions);
                if (responseAPI.MensajeSalida.Contains("CORRECTAMENTE"))
                {
                    // Retorna id solicitud ganadora
                    return Convert.ToInt32(responseAPI.MensajeSalida.Split("_")[1]);
                }

                Debug.WriteLine($"No fue un status 2XX: {response.StatusCode}");
                MessageBox.Show("No se ingreso subasta, vuelva a intentar");
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
                Debug.WriteLine($"Hubo un error {ex.Message}");
                MessageBox.Show("No se ingreso subasta, vuelva a intentar");
            }
        }
    }
}