using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace CloudDoorCs.Backend {

    public class BackendService {

        private const string PATH_REGISTER = "/api/bots";

        private static readonly HttpClient client = new HttpClient();

        private Uri backendUrl;
        public BackendService(Uri backendUrl) {
            this.backendUrl = backendUrl;
        }

        private StringContent toJsonContent(object obj) {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public RegistrationResultDto Register() {
            using(var result = client.PutAsync(new Uri(this.backendUrl, PATH_REGISTER), new ByteArrayContent(new byte[]{})).Result) {
                if(result.IsSuccessStatusCode) {
                    var resultResource = (RegistrationResultResource) JsonConvert.DeserializeObject(
                        result.Content.ReadAsStringAsync().Result, 
                        typeof(RegistrationResultResource)
                    );
                    return new RegistrationResultDto(resultResource);
                }
                throw new RegistrationException("Failed to register the user");
            }
        }
    }

}