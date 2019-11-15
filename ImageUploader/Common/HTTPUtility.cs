using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageUploader.Common
{
    public static class HTTPUtility
    {
        public async static Task<HttpResponseMessage> PostData(string url, object data) 
        {
            try 
            {
                using var client = new HttpClient();

                string json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                return response;
            }
            catch (HttpRequestException exception)
            {
                return new HttpResponseMessage 
                { 
                    StatusCode = HttpStatusCode.ServiceUnavailable, 
                    Content = new StringContent(exception.Message, Encoding.UTF8, "application/json")
            };
            }
        }
    }
}
