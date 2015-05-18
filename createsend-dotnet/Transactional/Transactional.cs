using System;

namespace createsend_dotnet.Transactional
{
    public interface ITransactional : IStatistics
    {
        IBasicEmail BasicEmail { get; }
        ISmartEmail SmartEmail { get; }
        IMessageBuilder MessageBuilder();
        IMessages Messages { get; }
    }

    public interface IAgencyTransactional : IAgencyStatistics
    {
        IAgencyBasicEmail BasicEmail { get; }
        IAgencySmartEmail SmartEmail { get; }
        IAgencyMessageBuilder MessageBuilder();
        IAgencyMessages Messages { get; }
    }

    internal class TransactionalContext : ITransactional, IAgencyTransactional
    {
        private readonly BasicEmailContext basicEmail;
        private readonly SmartEmailContext smartEmail;
        private readonly MessagesContext messages;
        private readonly StatisticsContext statistics;

        public TransactionalContext(AuthenticationDetails auth, ICreateSendOptions options)
        {
            basicEmail = new BasicEmailContext(auth, options);
            smartEmail = new SmartEmailContext(auth, options);
            messages = new MessagesContext(auth, options);
            statistics = new StatisticsContext(auth, options);
        }

        IBasicEmail ITransactional.BasicEmail
        {
            get { return basicEmail; }
        }

        IAgencyBasicEmail IAgencyTransactional.BasicEmail
        {
            get { return basicEmail; }
        }

        ISmartEmail ITransactional.SmartEmail
        {
            get { return smartEmail; }
        }

        IAgencySmartEmail IAgencyTransactional.SmartEmail
        {
            get { return smartEmail; }
        }
        
        IMessages ITransactional.Messages
        {
            get { return messages; }
        }

        IAgencyMessages IAgencyTransactional.Messages
        {
            get { return messages; }
        }

        IMessageBuilder ITransactional.MessageBuilder()
        {
            return new MessageBuilder(smartEmail, basicEmail);
        }

        IAgencyMessageBuilder IAgencyTransactional.MessageBuilder()
        {
            return new MessageBuilder(smartEmail, basicEmail);
        }

        public RateLimited<Statistics> Statistics(Guid? smartEmailId = null, string basicGroup = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client)
        {
            return statistics.Statistics(smartEmailId, basicGroup, from, to, timezone);
        }

        public RateLimited<Statistics> Statistics(string clientId, Guid? smartEmailId = null, string basicGroup = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client)
        {
            return statistics.Statistics(clientId, smartEmailId, basicGroup, from, to, timezone);
        }
    }
}
