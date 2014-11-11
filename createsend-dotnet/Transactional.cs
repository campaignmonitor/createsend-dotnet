using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Transactional : CreateSendBase
    {
        public string ClientId { get; set; }

        public Transactional(ICreateSendOptions options = null) : base(null, options) { }
        public Transactional(AuthenticationDetails auth, string clientId, ICreateSendOptions options = null)
            : base(auth, options)
        {
            ClientId = clientId;
        }

        public SendStatus SendBasic(SendBasicInfo info)
        {
            if (string.IsNullOrEmpty(ClientId))
            {
                throw new InvalidOperationException("Client Id cannot be null or empty");
            }

            return HttpPost<SendBasicInfo, SendStatus>(string.Format("/transactional/{0}/sendbasic", ClientId), null, info);
        }

        public SendStatus Send(SendTriggeredCampaign info)
        {
            if (string.IsNullOrEmpty(info.TriggeredCampaignId))
            {
                throw new InvalidOperationException("Triggered Campaign Id cannot be null or empty");
            }

            return HttpPost<SendTriggeredCampaign, SendStatus>(string.Format("/transactional/{0}/send", info.TriggeredCampaignId), null, info);
        }

        public QueryStatus Status(string sendId)
        {
            if (string.IsNullOrEmpty(sendId))
            {
                throw new InvalidOperationException("Send Id cannot be null or empty");
            }

            return HttpGet<QueryStatus>(string.Format("/transactional/{0}/status", sendId), null);
        }

        public List<TransactionalTimelineEvent> Timeline(TimelineQuery timelineQuery)
        {
            if (string.IsNullOrEmpty(ClientId))
            {
                throw new InvalidOperationException("Client Id cannot be null or empty");
            }

            var nameValueCollection = new NameValueCollection();
            if (timelineQuery.SinceId.HasValue)
                nameValueCollection.Add("SinceId", timelineQuery.SinceId.ToString());
            if (timelineQuery.Count.HasValue)
                nameValueCollection.Add("Count", timelineQuery.Count.ToString());
            if (timelineQuery.MaxId.HasValue)
                nameValueCollection.Add("MaxId", timelineQuery.MaxId.ToString());
            return HttpGet<List<TransactionalTimelineEvent>>(string.Format("/transactional/{0}/timeline", ClientId), nameValueCollection);
        }

        public TriggeredCampaignStatus AddTriggeredCampaign(TriggeredCampaign details)
        {
            if (string.IsNullOrEmpty(ClientId))
            {
                throw new InvalidOperationException("Client Id cannot be null or empty");
            }

            return HttpPost<TriggeredCampaign, TriggeredCampaignStatus>(string.Format("/transactional/{0}/addtriggeredcampaign", ClientId), null, details);
        }

        public TriggeredCampaign GetTriggeredCampaign(string triggeredCampaignId)
        {
            if (string.IsNullOrEmpty(triggeredCampaignId))
            {
                throw new InvalidOperationException("Triggered Campaign Id cannot be null or empty");
            }

            return HttpGet<TriggeredCampaign>(string.Format("/transactional/{0}", triggeredCampaignId), null);
        }

        public TriggeredCampaignStatus UpdateTriggeredCampaign(string triggeredCampaignId, TriggeredCampaign details)
        {
            if (string.IsNullOrEmpty(triggeredCampaignId))
            {
                throw new InvalidOperationException("Triggered Campaign Id cannot be null or empty");
            }

            return HttpPut<TriggeredCampaign, TriggeredCampaignStatus>(string.Format("/transactional/{0}", triggeredCampaignId), null, details);
        }

        public void DeleteTriggeredCampaign(string triggeredCampaignId)
        {
            if (string.IsNullOrEmpty(triggeredCampaignId))
            {
                throw new InvalidOperationException("Triggered Campaign Id cannot be null or empty");
            }

            HttpDelete(string.Format("/transactional/{0}/campaign.json", triggeredCampaignId), null);
        }
    }
}
