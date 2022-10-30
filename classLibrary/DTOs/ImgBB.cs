using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classLibrary.DTOs
{
    public class RequestImgBB
    {
        public string key { get; set; }
        public string image { get; set; }
        public string name { get; set; }

        public RequestImgBB(string key, string image, string name)
        {
            this.key = key;
            this.image = image;
            this.name = name;
        }
    }
    public class data
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("title")]
        public string title { get; set; }
        [JsonPropertyName("url")]
        public string url { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url_viewer")]
        public string UrlViewer { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("display_url")]
        public string DisplayUrl { get; set; }

        [JsonPropertyName("width")]
        public string Width { get; set; }

        [JsonPropertyName("height")]
        public string Height { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("expiration")]
        public string Expiration { get; set; }

        [JsonPropertyName("image")]
        public Image Image { get; set; }

        [JsonPropertyName("thumb")]
        public Thumb Thumb { get; set; }

        [JsonPropertyName("medium")]
        public Medium Medium { get; set; }

        [JsonPropertyName("delete_url")]
        public string DeleteUrl { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mime")]
        public string Mime { get; set; }

        [JsonPropertyName("extension")]
        public string Extension { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Medium
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mime")]
        public string Mime { get; set; }

        [JsonPropertyName("extension")]
        public string Extension { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
   public class ResponseImgBB
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }

    public class Thumb
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mime")]
        public string Mime { get; set; }

        [JsonPropertyName("extension")]
        public string Extension { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

}
