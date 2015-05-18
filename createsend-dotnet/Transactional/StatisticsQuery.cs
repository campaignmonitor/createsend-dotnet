using System;

namespace createsend_dotnet.Transactional
{
    public class StatisticsQuery
    {
        public Guid? SmartEmailId { get; set; }
        public string BasicGroup { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string TimeZone { get; set; }
    }
}
