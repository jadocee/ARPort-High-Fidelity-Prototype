using System;
using System.IO;
using System.Text;
using UnityEngine;
using _File = System.IO.File;
using File = UnityEngine.Windows.File;

namespace Controller
{
    public static class StorageController
    {
        public static void SaveToDisk(string filename, string json)
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            var data = Encoding.ASCII.GetBytes(json);
            File.WriteAllBytes(path, data);
        }

        public static string ReadFromDisk(string filename)
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            if (!File.Exists(path)) throw new FileNotFoundException(path);
            var data = File.ReadAllBytes(path);
            if (data == null) throw new NullReferenceException("Reading bytes returned null");
            var json = Encoding.ASCII.GetString(data);
            return json;
        }

        public static void DeleteFileFromDisk(string filename)
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            if (!File.Exists(path)) return;
            File.Delete(path);
        }
    }
}