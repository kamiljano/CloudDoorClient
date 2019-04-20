using System;
using System.IO;
using Newtonsoft.Json;

namespace CloudDoorCs.Local {

    public class FileService {

        private string ConfigPath {
            get {
                return Path.Combine(System.AppContext.BaseDirectory, "config.json");
            }
        }

        public Config SaveConfig(string connectionString, string deviceId) {
            var config = new Config {
                ConnectionString = connectionString,
                DeviceId = deviceId
            };
            using (StreamWriter file = new StreamWriter(ConfigPath)) {
                file.Write(JsonConvert.SerializeObject(config));
            }
            return config;
        }

        public Config LoadConfig() {
            var path = ConfigPath;
            try {
                using(StreamReader file = new StreamReader(path)) {
                    return JsonConvert.DeserializeObject(file.ReadToEnd(), typeof(Config)) as Config;
                }
            } catch (Exception e) {
                throw new ConfigLoadException(path, e);
            }
        }

    }

    public class ConfigLoadException : Exception {

        public ConfigLoadException(String path, Exception cause) : base($"Failed to load the configuration from {path}", cause) {}
    }
}