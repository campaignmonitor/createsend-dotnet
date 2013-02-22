using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class HistoryItem
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubscriberAction> Actions { get; set; }
    }

    public class SubscriberAction
    {
        public string Event { get; set; }
        public string IPAddress { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
    }
}
