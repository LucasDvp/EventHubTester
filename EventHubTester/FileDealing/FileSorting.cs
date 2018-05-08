using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubTester.FileDealing
{
    class FileSorting
    {
        public static void SortFileByTimeStamp(string inputFilePath, string outputFilePath)
        {
            //read json file
            var content = LocalFileAccess.ReadFile(inputFilePath);
            if (content == null) return ;
            var jsonArray = JsonConvert.DeserializeObject<List<BuildMsgTemplate>>(content);
            var orderedRsl = jsonArray.OrderBy(obj => obj.timeStamp);
            LocalFileAccess.WriteFile(outputFilePath, JsonConvert.SerializeObject(orderedRsl, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
        }

    }
}
