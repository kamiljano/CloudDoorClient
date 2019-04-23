using System.Collections.Generic;
using System.IO;

namespace CloudDoorCs.Local {

    public class DriveListingHandler : MethodCallHandler<DriveListingResponse, DriveListingRequest>
    {
        public DriveListingResponse handle(DriveListingRequest input)
        {
            List<DriveDto> result = new List<DriveDto>();
            
            foreach(var drive in DriveInfo.GetDrives()) {
                result.Add(new DriveDto {
                    name = drive.Name
                });  
            }

            return new DriveListingResponse {
                drives = result
            };
        }
    }

    public class DriveListingRequest {

    }

    public class DriveListingResponse {
        public List<DriveDto> drives;
    }

    public class DriveDto {

        public string name {get; internal set;}

    }

}