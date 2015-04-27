using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace createsend_dotnet.Transactional
{
    public interface IStatistics
    {
        RateLimited<Statistics> Statistics(Guid? smartEmailId = null, string basicGroup = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client);
    }

    public interface IAgencyStatistics : IStatistics
    {
        RateLimited<Statistics> Statistics(string clientId, Guid? smartEmailId = null, string basicGroup = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client); 
    }

    internal class StatisticsContext : CreateSendBase, IStatistics, IAgencyStatistics
    {
        public StatisticsContext(AuthenticationDetails authenticationDetails, ICreateSendOptions options)
            : base(authenticationDetails, options)
        {

        }

        public RateLimited<Statistics> Statistics(Guid? smartEmailId = null, string basicGroup = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client)
        {
            return Statistics(CreateQueryString(smartEmailId, basicGroup, from, to, timezone));
        }

        public RateLimited<Statistics> Statistics(string clientId, Guid? smartEmailId = null, string basicGroup = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client)
        {
            if (clientId == null) throw new ArgumentNullException("clientId");

            return Statistics(CreateQueryString(smartEmailId, basicGroup, from, to, timezone, clientId));
        }

        private RateLimited<Statistics> Statistics(NameValueCollection query)
        {
            return HttpGet<RateLimited<Statistics>>("/transactional/statistics", query);
        }

        private NameValueCollection CreateQueryString(Guid? smartEmailId, string basicGroup, DateTime? from, DateTime? to, DisplayedTimeZone timezone, string clientId = null)
        {
            return this.CreateQueryString(
                clientId,
                query: new NameValueCollection
                    {
                        { "smartemailid", smartEmailId.Encode() },
                        { "basicgroup", basicGroup.Encode() },
                        { "from", from.EncodeIso8601DateOnly() },
                        { "to", to.EncodeIso8601DateOnly() },
                        { "timezone", timezone.Encode() }
                    });
        }
    }

    public class Statistics
    {
        public StatisticsQuery Query { get; set; }
        public long Sent { get; set; }
        public long Bounces { get; set; }
        public long Delivered { get; set; }
        public long Opened { get; set; }
        public long Clicked { get; set; }
    }
}
