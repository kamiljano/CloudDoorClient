using System;

namespace CloudDoorCs.Backend {

    public class RegistrationException : Exception {
        public RegistrationException(string message) : base(message) {}
    }

}