using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace createsend_dotnet
{
    public class RateLimitStatus
    {
        public uint Remaining { get; set; }
        public uint Credit { get; set; }
        public uint Reset { get; set; }
    }

    public class EmailAddress
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
   
    public class Attachment
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }

    public class Image
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Bytes { get; set; }
    }

    public class SendBasicInfo
    {
        private const bool TrackOpensDefault = true;
        private const bool TrackClicksDefault = true;

        public EmailAddress From { get; set; }
        public EmailAddress ReplyTo { get; set; }
        public EmailAddress To { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public Image[] Images { get; set; }
        public Attachment[] Attachments { get; set; }
        public bool TrackOpens { get; set; }
        public bool TrackClicks { get; set; }
        public bool InlineCSS { get; set; }
        public string SubscriberListToJoin { get; set; }
        public string EmailGroupName { get; set; }

        public SendBasicInfo()
        {
            TrackOpens = TrackOpensDefault;
            TrackClicks = TrackClicksDefault;
            InlineCSS = true;
        }
    }

    public class EmailVariable
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class EmailVariables : KeyedCollection<string, EmailVariable>
    {
        public EmailVariables()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        protected override string GetKeyForItem(EmailVariable item)
        {
            return item.Name;
        }
    }
    
    public class SendTriggeredCampaign
    {
        public EmailAddress To { get; set; }
        public string TriggeredCampaignId { get; set; }
        public EmailVariables EmailVariables { get; set; }

        public SendTriggeredCampaign(EmailAddress to, string triggeredCampaignId, EmailVariables emailVariables = null)
        {
            To = to;
            TriggeredCampaignId = triggeredCampaignId;
            EmailVariables = emailVariables;
        }
    }

    public class SendStatus
    {
        public string SendId { get; set; }
        public string Status { get; set; }
        public RateLimitStatus RateLimitStatus { get; set; }
    }

    public class QueryStatus
    {
        public string Status { get; set; }
        public RateLimitStatus RateLimitStatus { get; set; }
    }

    public class TimelineQuery
    {
        public long? SinceId { get; set; }
        public int? Count { get; set; }
        public long? MaxId { get; set; }
    }

    public class TransactionalTimelineEvent
    {
        public long EventId { get; set; }
        public Guid MessageId { get; set; }
        public string EventType { get; set; }
        public DateTime EventTime { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public int Opens { get; set; }
        public int Clicks { get; set; }
    }

    public class TriggeredCampaign
    {
        public string Name { get; set; }
        public EmailAddress From { get; set; }
        public EmailAddress ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public Image[] Images { get; set; }
    }

    public class TriggeredCampaignStatus
    {
        public string TriggeredCampaignId { get; set; }
        public string Status { get; set; }
        public RateLimitStatus RateLimitStatus { get; set; }
    }
}
