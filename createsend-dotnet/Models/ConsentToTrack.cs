using Newtonsoft.Json;

namespace createsend_dotnet
{
    [JsonConverter(typeof(ConsentToTrackEnumConverter))]
    public enum ConsentToTrack
    {
        Unchanged = 0,
        Yes = 1,
        No = 2
    }
}
