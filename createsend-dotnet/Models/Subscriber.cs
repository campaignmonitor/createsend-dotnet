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

    public class SubscriberCustomField
    {
        public string Key { get; set; }
        public string Value{ get ;set; }
    }

    public class Subscriber : BasicSubscriber
    {
        public string Name { get; set; }
        public List<SubscriberCustomField> CustomFields { get; set; }
    }
}
