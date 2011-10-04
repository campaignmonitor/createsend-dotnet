using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class CampaignDetail
    {
        public string CampaignID { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string SentDate { get; set; }
        public int TotalRecipients { get; set; }
        public string WebVersionURL { get; set; }
    }

    public class ScheduledCampaignDetail
    {
        public string CampaignID { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string PreviewURL { get; set; }
        public string DateScheduled { get; set; }
        public string ScheduledTimeZone { get; set; }
    }

    public class DraftDetail
    {
        public string CampaignID { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string PreviewURL { get; set; }
    }

    public class CampaignSummary
    {
        public int Recipients { get; set; }
        public int TotalOpened { get; set; }
        public int Clicks { get; set; }
        public int Unsubscribed { get; set; }
        public int Bounced { get; set; }
        public int UniqueOpened { get; set; }
        public string WebVersionURL { get; set; }
    }

    public class CampaignOpenDetail
    {
        public string EmailAddress { get; set; }
        public string ListID { get; set; }
        public DateTime Date { get; set; }
        public string IPAddress { get; set; }
    }

    public class CampaignUnsubscribeDetail : CampaignOpenDetail { }

    public class CampaignClickDetail : CampaignOpenDetail
    {
        public string URL { get; set; }
    }

    public class CampaignBounceDetail : CampaignOpenDetail
    {
        public string BounceType { get; set; }
        public string Reason { get; set; }
    }

    public class CampaignListsAndSegments
    {
        public Segments Segments { get; set; }
        public Lists Lists { get; set; }
    }
}
