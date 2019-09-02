
namespace createsend_dotnet
{
    public class Journey : CreateSendBase
    {
        public string JourneyID { get; set; }

        public Journey(AuthenticationDetails auth, string journeyID)
            : base(auth)
        {
            JourneyID = journeyID;
        }

        public JourneySummary Summary()
        {
            return HttpGet<JourneySummary>(string.Format("/journeys/{0}.json", JourneyID), null);
        }
    }
}
