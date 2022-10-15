using System;
using System.IO;
using System.Text;
using UnityEngine;
using _File = System.IO.File;
using File = UnityEngine.Windows.File;

namespace Controller
{
    public class StorageController
    {
        public void SaveToDisk(string filename, string json)
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            // using TextWriter textWriter = _File.CreateText(path);
            // textWriter.Write(json);
            var data = Encoding.ASCII.GetBytes(json);
            File.WriteAllBytes(path, data);
        }

        public string ReadFromDisk(string filename)
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            var data = File.ReadAllBytes(path);
            if (data == null) throw new NullReferenceException("Reading bytes returned null");
            var json = Encoding.ASCII.GetString(data);
            return json;
        }
    }
}