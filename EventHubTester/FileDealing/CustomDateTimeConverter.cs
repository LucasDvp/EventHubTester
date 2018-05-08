using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubTester.FileDealing
{
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        private string format = "yyyy-MM-dd HH:mm:ss.fff";
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime dt;
            if (DateTime.TryParseExact(reader.Value.ToString(), this.format, CultureInfo.InvariantCulture,
                           DateTimeStyles.None,
                           out dt))
            {
                return dt;
            }
            else
            {
                Console.WriteLine($"invalide datetime string:{reader.Value.ToString()}");
                return dt;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(this.format));
        }
    }
}
