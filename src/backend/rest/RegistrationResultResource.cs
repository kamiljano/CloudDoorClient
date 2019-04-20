using Newtonsoft.Json;

namespace CloudDoorCs.Backend {

    internal class RegistrationResultResource {

        [JsonProperty(PropertyName = "connectionString")]
        internal string ConnectionString {get; set;}

        [JsonProperty(PropertyName = "deviceId")]
        internal string DeviceId {get; set;}

    }

}