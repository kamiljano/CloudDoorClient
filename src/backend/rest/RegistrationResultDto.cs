namespace CloudDoorCs.Backend {

    public class RegistrationResultDto {

        public string ConnectionString {get;}

        public string DeviceId {get;}

        internal RegistrationResultDto(RegistrationResultResource resource) {
            this.ConnectionString = resource.ConnectionString;
            this.DeviceId = resource.DeviceId;
        }
    }

}