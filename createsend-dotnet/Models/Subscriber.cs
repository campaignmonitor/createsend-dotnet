using System;
using System.Collections.Generic;

namespace createsend_dotnet
{
    public class BasicSubscriber
    {
        public string EmailAddress { get; set; }
        public string State { get; set; }
    }

    public class SuppressedSubscriber : BasicSubscriber
    {
        public DateTime Date { get; set; }
        public string SuppressionReason { get; set; }
    }

    public class CampaignRecipient
    {
        public string EmailAddress { get; set; }
        public string ListID { get; set; }
    }

    public class CampaignRecipients : List<CampaignRecipient>
    {
    }

    public class SubscriberCustomField
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Clear { get; set; }
    }

    public class SubscriberDetail : BasicSubscriber
    {
        public SubscriberDetail()
            : base()
        {
        }

        public SubscriberDetail(
            string emailAddress,
            string name,
            List<SubscriberCustomField> customFields)
        {
            EmailAddress = emailAddress;
            Name = name;
            CustomFields = customFields;
        }
        public DateTime StateChangedDate { get; set; }
        public DateTime ListJoinedDate { get; set; }
        public string Name { get; set; }
        public List<SubscriberCustomField> CustomFields { get; set; }
        public string ReadsEmailWith { get; set; }
        public ConsentToTrack? ConsentToTrack { get; set; }
    }
}
