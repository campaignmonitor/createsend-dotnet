using System;
using System.Collections.Specialized;

namespace createsend_dotnet.Transactional
{
    public interface IMessages
    {
        RateLimited<RecipientStatus> Resend(Guid messageId);
        RateLimited<MessageDetail> Details(Guid messageId, bool statistics = false);
        RateLimited<MessageListDetail[]> List(Guid? sentAfterId = null, Guid? sentBeforeId = null, int count = 50, string basicGroup = null, MessageListStatus status = MessageListStatus.All);
    }

    public interface IAgencyMessages : IMessages
    {
        RateLimited<MessageListDetail[]> List(string clientId, Guid? sentAfterId = null, Guid? sentBeforeId = null, int count = 50, string basicGroup = null, MessageListStatus status = MessageListStatus.All);
    }

    internal class MessagesContext : CreateSendBase, IMessages, IAgencyMessages
    {
        public MessagesContext(AuthenticationDetails authenticationDetails, ICreateSendOptions options)
            : base(authenticationDetails, options)
        {

        }

        public RateLimited<RecipientStatus> Resend(Guid messageId)
        {
            if (messageId == Guid.Empty) throw new ArgumentException("Must not be empty", "messageId");

            return Resend(messageId, new NameValueCollection());
        }

        private RateLimited<RecipientStatus> Resend(Guid messageId, NameValueCollection query)
        {
            return HttpPost<object, RateLimited<RecipientStatus>>(String.Format("/transactional/messages/{0}/resend", messageId), query, null);
        }

        public RateLimited<MessageDetail> Details(Guid messageId, bool statistics = false)
        {
            return Details(messageId, CreateQueryString(statistics));
        }

        private RateLimited<MessageDetail> Details(Guid messageId, NameValueCollection query)
        {
            return HttpGet<RateLimited<MessageDetail>>(String.Format("/transactional/messages/{0}", messageId), query);
        }

        public RateLimited<MessageListDetail[]> List(Guid? sentAfterId = null, Guid? sentBeforeId = null, int count = 50, string basicGroup = null, MessageListStatus status = MessageListStatus.All)
        {
            return ListMessages(CreateQueryString(basicGroup, sentAfterId, sentBeforeId, count, status));
        }

        public RateLimited<MessageListDetail[]> List(string clientId, Guid? sentAfterId = null, Guid? sentBeforeId = null, int count = 50, string basicGroup = null, MessageListStatus status = MessageListStatus.All)
        {
            if (clientId == null) throw new ArgumentNullException("clientId");

            return ListMessages(CreateQueryString(basicGroup, sentAfterId, sentBeforeId, count, status, clientId));
        }

        private RateLimited<MessageListDetail[]> ListMessages(NameValueCollection query)
        {
            return HttpGet<RateLimited<MessageListDetail[]>>("/transactional/messages", query);
        }

        private NameValueCollection CreateQueryString(bool statistics)
        {
            return new NameValueCollection
                    {
                        { "statistics", statistics.Encode() },
                    };
        }

        private NameValueCollection CreateQueryString(string basicGroup, Guid? sentAfterId, Guid? sentBeforeId, int count, MessageListStatus status, string clientId = null)
        {
            return this.CreateQueryString(
                clientId,
                query: new NameValueCollection
                    {
                        { "basicgroup", basicGroup.Encode() },
                        { "sentAfterId", sentAfterId.Encode() },
                        { "sentBeforeId", sentBeforeId.Encode() },
                        { "count", count.Encode() },
                        { "status", status.Encode() },
                    });
        }
    }
}
