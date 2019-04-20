using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;

namespace CloudDoorCs.Local {

    public class MessageBroker {

        private DeviceClient client;

        public MessageBroker(DeviceClient client) {
            this.client = client;
        }

        public MessageBroker AddListener<RES, REQ>(string name, MethodCallHandler<RES, REQ> handler) {        
            client.SetMethodHandlerAsync(name, async (MethodRequest, context) => {
                var input = (REQ) JsonConvert.DeserializeObject(MethodRequest.DataAsJson, typeof(REQ));
                var result = handler.handle(input);
                var resultBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                return new MethodResponse(resultBytes, 200);
            }, null).Wait();
            return this;
        }
    }

}