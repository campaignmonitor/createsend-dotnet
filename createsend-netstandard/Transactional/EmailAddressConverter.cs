using System;
using Newtonsoft.Json;

namespace createsend_dotnet.Transactional
{
    internal class EmailAddressConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EmailAddress);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return new EmailAddress((string)reader.Value);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null || value.GetType() != typeof(EmailAddress))
            {
                writer.WriteNull();
            }
            else
            {
                EmailAddress e = (EmailAddress)value;
                writer.WriteValue(string.Format("{0} <{1}>", e.Name, e.Email));
            }
        }
    }
}