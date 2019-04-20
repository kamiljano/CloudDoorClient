using CloudDoorCs.Local;

namespace CloudDoorCs {

    public class BotConfig {
        public string ConnectionString {get;}

        public string DeviceId {get;}

        public BotConfig(Config config) {
            this.ConnectionString = config.ConnectionString;
            this.DeviceId = config.DeviceId;
        }
    }

}