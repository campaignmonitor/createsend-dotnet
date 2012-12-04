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
        public string WebVersionTextURL { get; set; }
    }

    public class ScheduledCampaignDetail
    {
        public string CampaignID { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string PreviewURL { get; set; }
        public string PreviewTextURL { get; set; }
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
        public string PreviewTextURL { get; set; }
    }

    public class CampaignSummary
    {
        public int Recipients { get; set; }
        public int TotalOpened { get; set; }
        public int Clicks { get; set; }
        public int Unsubscribed { get; set; }
        public int SpamComplaints { get; set; }
        public int Bounced { get; set; }
        public int UniqueOpened { get; set; }
        public int Mentions { get; set; }
        public int Forwards { get; set; }
        public int Likes { get; set; }
        public string WebVersionURL { get; set; }
        public string WebVersionTextURL { get; set; }
        public string WorldviewURL { get; set; }
    }

    public class CampaignDetailBase
    {
        public string EmailAddress { get; set; }
        public string ListID { get; set; }
        public DateTime Date { get; set; }
    }

    public class CampaignDetailBaseWithIPAddress : CampaignDetailBase
    {
        public string IPAddress { get; set; }
    }

    public class CampaignDetailWithGeoBase : CampaignDetailBaseWithIPAddress
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class CampaignOpenDetail : CampaignDetailWithGeoBase { }

    public class CampaignClickDetail : CampaignDetailWithGeoBase
    {
        public string URL { get; set; }
    }

    public class CampaignUnsubscribeDetail : CampaignDetailBaseWithIPAddress { }

    public class CampaignSpamComplaint : CampaignDetailBase { }

    public class CampaignBounceDetail : CampaignDetailBaseWithIPAddress
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
