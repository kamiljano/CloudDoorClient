namespace CloudDoorCs.Local {

    public class PingPongHandler : MethodCallHandler<PingPongResponse, PingPongRequest> {
        
        const string PONG = "PONG";

        public PingPongResponse handle(PingPongRequest input) {
            return new PingPongResponse {
                Message = PONG
            };
        }

    }

    public class PingPongRequest {

        public string Message {get; set;}

    }

    public class PingPongResponse {

        public string Message {get; internal set;}

    }
}