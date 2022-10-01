using classLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace app.Data.Implementations
{
    internal class BaseDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public BaseDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = "https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/dev/";
            _jsonSerializerOptions = new JsonSerializerOptions();
        }


        public async Task<object> TraerEntidades(string procedimiento, Type tipoLista)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(String.Concat(_baseAddress, $"{procedimiento}/"), new { });

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != string.Empty)
                    {

                        var entidades = JsonSerializer.Deserialize<Type>(content, _jsonSerializerOptions);
                        return entidades;
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

        public Task ActualizarEntidad(Type tipoEntidad)
        {
            throw new NotImplementedException();
        }

        public Task BorrarEntidad(string id)
        {
            throw new NotImplementedException();
        }

        public Task CrearEntidad(Type entidad)
        {
            throw new NotImplementedException();
        }
    }
}
