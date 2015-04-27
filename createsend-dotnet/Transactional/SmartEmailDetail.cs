using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace createsend_dotnet.Transactional
{
    public class SmartEmailDetail
    {
        public Guid SmartEmailId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Status { get; set; }
        public SmartEmailProperties Properties { get; set; }
    }
}
