using Bus_backUpData.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Func
{
    public static class LibrarySettingFileConfig
    {
        public static Stream Createfilejson(object listConfig)
        {
            var stream = new MemoryStream();
            using (var streamWriter = new StreamWriter(stream: stream, encoding: Encoding.UTF8, bufferSize: 4096, leaveOpen: true)) // last parameter is important
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                var serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(jsonWriter, listConfig);
                streamWriter.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }
        public static void SaveFileStream(string path, Stream stream)
        {
            string pathLocation = Path.GetFullPath("Config");
            var fileStream = new FileStream(pathLocation+"\\" + path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }
        public static void SaveConfig(object listConfig, string path)
        {
          var file =  Createfilejson(listConfig);
            SaveFileStream(path,file);
        }
    }
}
