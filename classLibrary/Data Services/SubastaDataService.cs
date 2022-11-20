using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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


        public async Task<bool> CrearSubasta(CrearSubasta crearSubasta)
        {
            try
            {
                string jsonSubasta = JsonSerializer.Serialize<CrearSubasta>(crearSubasta, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonSubasta, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await _httpClient.PostAsync($"{_baseAddress}sp_insert_subasta/", content);
                
                var result = response.Content.ReadAsStringAsync().Result;
                var responseAPI = JsonSerializer.Deserialize<ResponseGeneral>(result, _jsonSerializerOptions);
                if (responseAPI.MensajeSalida.Contains("CORRECTAMENTE"))
                {
                    Debug.WriteLine("Subasta creada!");
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
            }

            return false;
        }
    }
}