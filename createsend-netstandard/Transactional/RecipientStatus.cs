using System;

namespace createsend_dotnet.Transactional
{
    public class RecipientStatus
    {
        public Guid MessageId { get; set; }
        public EmailAddress Recipient { get; set; }
        public string Status { get; set; }
    }
}
