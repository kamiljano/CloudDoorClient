using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Management;
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
            collection["specs"] = getSpecs();
            //TODO: add CPU, memory, computer names
            return collection;
        }

        private TwinCollection getSpecs() {
            TwinCollection result = new TwinCollection();
            result["cpus"] = getCpus();
            result["memory"] = getMemory();
            return result;
        }

        private TwinCollection getCpus() {
            TwinCollection cpus = new TwinCollection();
            ManagementObjectSearcher proc = new ManagementObjectSearcher("select * from Win32_Processor");
            int i = 0;
            foreach(ManagementObject obj in proc.Get()) {
                TwinCollection cpu = new TwinCollection();
                cpu["name"] = obj["Name"];
                cpu["cores"] = obj["NumberOfCores"];
                cpu["frequency"] = obj["CurrentClockSpeed"];
                cpus["cpu" + (i++)] = cpu;
            }
            return cpus;
        }

        private double getMemory() {
            ManagementObjectSearcher result = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            double sum = 0;
            foreach(ManagementObject obj in result.Get()) {
                double kbs = Convert.ToDouble(obj["TotalVisibleMemorySize"]);
                sum += Math.Round(kbs / 1024, 2);
            }
            return sum;
        }

        public Task UploadFile(string path) {
            return client.UploadToBlobAsync(path, File.OpenRead(path));
        }
    }
}