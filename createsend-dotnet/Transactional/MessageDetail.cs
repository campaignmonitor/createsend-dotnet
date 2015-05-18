using System;

namespace createsend_dotnet.Transactional
{
    public class MessageDetail
    {
        public Guid MessageId { get; set; }
        public string Status { get; set; }
        public DateTimeOffset SentAt { get; set; }

        public bool CanBeResent { get; set; }
        public EmailAddress Recipient { get; set; }

        public MessageContent Message { get; set; }
        public string BasicGroup { get; set; }
        public Guid? SmartEmailId { get; set; }
        
        public long TotalOpens { get; set; }
        public long TotalClicks { get; set; }
        
        public SubscriberAction[] Opens { get; set; }
        public SubscriberAction[] Clicks { get; set; }
    }
}
