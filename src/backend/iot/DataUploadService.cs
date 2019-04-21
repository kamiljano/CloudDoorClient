using System;
using System.Text;
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
            return collection;
        }
    }
}