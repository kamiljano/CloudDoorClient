using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudDoorCs.Local {

    public class DirectoryListingHandler : MethodCallHandler<DirectoryListingResponse, DirectoryListingRequest>
    {
        public DirectoryListingResponse handle(DirectoryListingRequest input)
        {
            string path = input.path.EndsWith("/") ? input.path : input.path + "/";
            return new DirectoryListingResponse {
                files = getFiles(path)
            };
        }

        private List<FileDto> getFiles(string path) {
            var result = new List<FileDto>();

            foreach (var dir in Directory.GetDirectories(path)) {
                result.Add(new FileDto{
                    type = FileType.DIR,
                    name = Path.GetFileName(dir)
                });
            }

            foreach (var file in Directory.GetFiles(path)) {
                result.Add(new FileDto{
                    type = FileType.FILE,
                    name = Path.GetFileName(file)
                });
            }
            return result;
        }
    }

    public class DirectoryListingResponse {
        
        public List<FileDto> files {get; internal set;}

    }

    public class FileDto {

        public string name {get; internal set;}
        
        [JsonConverter(typeof(StringEnumConverter))]
        public FileType type {get; internal set;}
    }

    public enum FileType {

        FILE,
        DIR

    }

    public class DirectoryListingRequest {
        public string path {get; set;}

    }

}