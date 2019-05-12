using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CloudDoorCs.Backend;
using CloudDoorCs.Local.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudDoorCs.Local {

    public class FileUploadHandler : MethodCallHandler<FileUploadResponse, FileUploadRequest>
    {
        private readonly DataUploadService dataUploadService;

        public FileUploadHandler(DataUploadService dataUploadService) {
            this.dataUploadService = dataUploadService;
        }

        public FileUploadResponse handle(FileUploadRequest input)
        {
            List<Task<UploadResult>> tasks = new List<Task<UploadResult>>(input.paths.Count);
            foreach (var path in input.paths) {
                tasks.AddRange(upload(path));
            }
            Task.WaitAll(tasks.ToArray());
            List<UploadResult> result = new List<UploadResult>(input.paths.Count);
            foreach(var task in tasks) {
                result.Add(task.Result);
            }

            return new FileUploadResponse {
                uploads = result
            };
        }

        private List<Task<UploadResult>> upload(string path) {
            if (!File.Exists(path) && !Directory.Exists(path)) {
                return new List<Task<UploadResult>> {Task.FromResult(new UploadResult{
                    path = path,
                    status = UploadStatus.DOES_NOT_EXIST
                })};
            }
            var attrs = File.GetAttributes(path);
            if (attrs.HasFlag(FileAttributes.Directory)) {
                return uploadDir(path);
            } else {
                return new List<Task<UploadResult>> { uploadFile(path) };
            }
        }

        private List<Task<UploadResult>> uploadDir(string path) {
            var files = IOUtils.ListFilesRecursively(path);
            List<Task<UploadResult>> result = new List<Task<UploadResult>>(files.Count);
            foreach(var file in files) {
                result.Add(uploadFile(file));
            }
            return result;
        }

        private Task<UploadResult> uploadFile(string path) {
            return dataUploadService.UploadFile(path).ContinueWith(t => {
               return new UploadResult{
                   path = path,
                   status = t.IsCompletedSuccessfully ? UploadStatus.UPLOADED : UploadStatus.FAILED
               };
            });
        }
    }

    public class FileUploadRequest {
        public List<string> paths{get; set;}
    }

    public class UploadResult {

        [JsonConverter(typeof(StringEnumConverter))]
        public UploadStatus status{get; set;}

        public string path{get; internal set;}

    }

    public enum UploadStatus {
        DOES_NOT_EXIST,
        UPLOADED,
        FAILED
    }

    public class FileUploadResponse {
        public List<UploadResult> uploads {get; internal set;}
    }

}