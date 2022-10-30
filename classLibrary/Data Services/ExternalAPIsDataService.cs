using classLibrary.DTO;
using classLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public class ExternalAPIsDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _imgBBKey = "c5e06383708ec42092d15248d26eaf46";
        private readonly string _baseImgBB = "https://api.imgbb.com/1/upload";
;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Instancia el data service con la direccion base (para no repetirla en cada llamada)
        /// </summary>
        public ExternalAPIsDataService()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions();
        }
        public async Task<ResponseImgBB> SubirImagen(string base64Imagen, string nombre)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.Timeout = TimeSpan.FromSeconds(300);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new(_baseImgBB);

            var content = new MultipartFormDataContent();

            content.Add(new StringContent(_imgBBKey), "key");
            content.Add(new StringContent(base64Imagen), "image");
            // Ya que el nombre es un parametro opcional lo agregamos al cuerpo solo si es valido
            if (nombre != String.Empty || nombre != null)
            {
                content.Add(new StringContent(nombre), "name");
            }

            request.Content = content;
            var header = new ContentDispositionHeaderValue("form-data");
            request.Content.Headers.ContentDisposition = header;

            var response = await _httpClient.PostAsync(request.RequestUri.ToString(), request.Content);
            var result = response.Content.ReadAsStringAsync().Result;


            //TODO Ver como mapear respuesta
            var responseAPI = JsonSerializer.Deserialize<ResponseImgBB>(result, _jsonSerializerOptions);
            if (response.IsSuccessStatusCode)
            {
                return responseAPI;

            }
            return null;
        }
    }
}
