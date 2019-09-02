using System.Collections.Generic;

namespace createsend_dotnet
{
    public class JourneyDetail
    {
        public string ListID { get; set; }
        public string JourneyID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class JourneySummary
    {
        public string JourneyID { get; set; }
        public string Name { get; set; }
        public string TriggerType { get; set; }
        public string Status { get; set; }
        public List<JourneyEmailSummary> Emails { get; set; }
    }

    public class JourneyEmailSummary
    {
        public string EmailID { get; set; }
        public string Name { get; set; }
        public int Sent { get; set; }
        public int Opened { get; set; }
        public int Clicked { get; set; }
        public int Unsubscribed { get; set; }
        public int Bounced { get; set; }
        public int UniqueOpened { get; set; }
    }
}
