using System;
using System.Text;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace CloudDoorCs.Backend {

    public class DataUploadService {

        private DeviceClient client;

        public DataUploadService(DeviceClient client) {
            this.client = client;
        }

        public void PublishDeviceInfo() {
            var deviceData = getDeviceData();
            this.client.SendEventAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(deviceData))));
        }

        private DeviceDataDto getDeviceData() {
            return new DeviceDataDto {
                os = new Os {
                    type = Environment.OSVersion.Platform.ToString(),
                    version = Environment.OSVersion.VersionString
                },
                currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name
            };
        }
    }

    internal class DeviceDataDto {

        internal Os os { get; set; }

        internal string currentUser { get; set; }

    }

    internal class Os {

        internal string version {get; set;}

        internal string type {get; set;}

    }
}