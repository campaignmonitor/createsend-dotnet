using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace createsend_dotnet
{
    public class ConsentToTrackEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || string.IsNullOrEmpty(reader.Value.ToString()))
                return null;

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}