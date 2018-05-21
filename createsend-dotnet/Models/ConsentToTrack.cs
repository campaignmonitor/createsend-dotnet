using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace createsend_dotnet
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConsentToTrack
    {
        Unchanged = 0,
        Yes = 1,
        No = 2
    }
}
