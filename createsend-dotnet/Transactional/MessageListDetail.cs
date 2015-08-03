using System;

namespace createsend_dotnet.Transactional
{
    public class MessageListDetail
    {
        public Guid MessageId { get; set; }
        public EmailAddress Recipient { get; set; }
        public EmailAddress From { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public string Group { get; set; }
        public Guid? SmartEmailId { get; set; }
        public long TotalOpens { get; set; }
        public long TotalClicks { get; set; }
        public bool CanBeResent { get; set; }
    }
}
