using System;

namespace createsend_dotnet.Transactional
{
    public class SmartEmailListDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
