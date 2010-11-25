using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class BasicSubscriber
    {
        public string EmailAddress { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
    }

    public class CampaignRecipient
    {
        public string EmailAddress { get; set; }
        public string ListID { get; set; }
    }

    public class CampaignRecipients : List<CampaignRecipient> { }

    public class SubscriberCustomField
    {
        public string Key { get; set; }
        public string Value{ get ;set; }
    }

    public class SubscriberDetail : BasicSubscriber
    {
        public SubscriberDetail() : base() { }

        public SubscriberDetail(string emailAddress, string name, List<SubscriberCustomField> customFields)
        {
            EmailAddress = emailAddress;
            Name = name;
            CustomFields = customFields;
        }

        public string Name { get; set; }
        public List<SubscriberCustomField> CustomFields { get; set; }
    }
}
