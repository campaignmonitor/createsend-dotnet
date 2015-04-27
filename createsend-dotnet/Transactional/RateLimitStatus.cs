using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace createsend_dotnet.Transactional
{
    public class RateLimitStatus
    {
        public uint Remaining { get; set; }
        public uint Credit { get; set; }
        public uint Reset { get; set; }
    }
}
