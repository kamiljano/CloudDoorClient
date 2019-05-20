using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
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

        private List<AbstractFileDto> getFiles(string path) {
            var result = new List<AbstractFileDto>();

            foreach (var dir in Directory.GetDirectories(path)) {
                result.Add(new DirDto {
                    name = Path.GetFileName(dir)
                });
            }

            foreach (var file in Directory.GetFiles(path)) {
                var fileInfo = new FileInfo(file);
                result.Add(new FileDto{
                    name = Path.GetFileName(file),
                    size = fileInfo.Length,
                    lastTouch = fileInfo.LastWriteTimeUtc
                });
            }
            return result;
        }
    }

    public class DirectoryListingResponse {
        
        public List<AbstractFileDto> files {get; internal set;}

    }

    public abstract class AbstractFileDto {

        public string name {get; internal set;}

    }

    public class DirDto : AbstractFileDto {

        [JsonConverter(typeof(StringEnumConverter))]
        public FileType type {
            get {
                return FileType.DIR;
            }
        }
    }

    public class FileDto : AbstractFileDto {
        
        [JsonConverter(typeof(StringEnumConverter))]
        public FileType type {
            get {
                return FileType.FILE;
            }
        }

        public long size {get; internal set;}

        public DateTime lastTouch {get; internal set;}
    }

    public enum FileType {

        FILE,
        DIR

    }

    public class DirectoryListingRequest {
        public string path {get; set;}

    }

}