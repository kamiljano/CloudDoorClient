
using System;
using System.Collections.Generic;
using System.Text;
using CloudDoorCs.Backend;
using CloudDoorCs.Local;
using Microsoft.Azure.Devices.Client;

namespace CloudDoorCs {

    public class Bot {

        private readonly BackendService backendService;
        private readonly FileService fileService;

        public Bot(BackendService backendService, FileService fileService) {
            this.backendService = backendService;
            this.fileService = fileService;
        }
        
        public void Start() {
            BotConfig config = getConfig();
            var client = DeviceClient.CreateFromConnectionString(config.ConnectionString, config.DeviceId, TransportType.Mqtt); //todo: other protocols
            new DataUploadService(client).PublishDeviceInfo();
            new MessageBroker(client)
                .AddListener("ping", new PingPongHandler())
                .AddListener("driveList", new DriveListingHandler())
                .AddListener("dirList", new DirectoryListingHandler());
            Console.ReadLine();
        }

        private BotConfig getConfig() {
            try {
                return new BotConfig(fileService.LoadConfig());
            } catch (ConfigLoadException e) {
                return new BotConfig(Register());
            }
        }

        private Config Register() {
            var dto = backendService.Register();
            //TODO: publish client information and extract a registration service
            return fileService.SaveConfig(dto.ConnectionString, dto.DeviceId);
        }
    }

}