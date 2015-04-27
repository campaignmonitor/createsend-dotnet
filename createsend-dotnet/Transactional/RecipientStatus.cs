using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace createsend_dotnet.Transactional
{
    public class RecipientStatus
    {
        public Guid MessageId { get; set; }
        public EmailAddress Recipient { get; set; }
        public string Status { get; set; }
    }
}
