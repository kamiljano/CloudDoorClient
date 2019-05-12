using System;
using System.Collections.Generic;
using System.IO;

namespace CloudDoorCs.Local.Utils {

    public static class IOUtils {

        public static List<string> ListFilesRecursively(string rootPath) {
            List<string> files = new List<string>();
            ListFilesRecursively(rootPath, files);
            return files;
        }

        public static void ListFilesRecursively(string rootPath, List<string> result) {
            try {
                foreach (string f in Directory.GetFiles(rootPath)) {
                    result.Add(f);
                }
            } catch (UnauthorizedAccessException e) {
                Console.Error.WriteLine("Failed to read the files from " + rootPath);
            }
            try {
                foreach (string dir in Directory.GetDirectories(rootPath)) {
                    ListFilesRecursively(dir, result);
                }
            } catch (UnauthorizedAccessException e) {
                Console.Error.WriteLine("Failed to read the sub-directories from " + rootPath);
            }
        }
    }
}