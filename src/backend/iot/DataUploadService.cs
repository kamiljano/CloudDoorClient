using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;

namespace CloudDoorCs.Backend {

    public class DataUploadService {

        private readonly DeviceClient client;

        public DataUploadService(DeviceClient client) {
            this.client = client;
        }

        public void PublishDeviceInfo() {
            var deviceData = getDeviceData();
            client.UpdateReportedPropertiesAsync(deviceData);
        }

        private TwinCollection getDeviceData() {
            var collection = new TwinCollection();
            collection["os"] = new TwinCollection();
            collection["os"]["version"] = Environment.OSVersion.VersionString;
            collection["os"]["type"] = Environment.OSVersion.Platform.ToString();
            collection["currentUser"] = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //TODO: add CPU, memory, computer names
            return collection;
        }

        public Task UploadFile(string path) {
            return client.UploadToBlobAsync(path, File.OpenRead(path));
        }
    }
}