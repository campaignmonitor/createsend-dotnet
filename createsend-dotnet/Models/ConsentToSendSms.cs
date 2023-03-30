using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConsentToSendSms
    {
        Unchanged = 0,
        Yes = 1,
        No = 2
    }
}
