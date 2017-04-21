using System;

namespace createsend_dotnet.Transactional
{
    public interface ITransactional : IStatistics
    {
        IClassicEmail ClassicEmail { get; }
        ISmartEmail SmartEmail { get; }
        IMessageBuilder MessageBuilder();
        IMessages Messages { get; }
    }

    public interface IAgencyTransactional : IAgencyStatistics
    {
        IAgencyClassicEmail ClassicEmail { get; }
        IAgencySmartEmail SmartEmail { get; }
        IAgencyMessageBuilder MessageBuilder();
        IAgencyMessages Messages { get; }
    }

    internal class TransactionalContext : ITransactional, IAgencyTransactional
    {
        private readonly ClassicEmailContext classicEmail;
        private readonly SmartEmailContext smartEmail;
        private readonly MessagesContext messages;
        private readonly StatisticsContext statistics;

        public TransactionalContext(AuthenticationDetails auth, ICreateSendOptions options)
        {
            classicEmail = new ClassicEmailContext(auth, options);
            smartEmail = new SmartEmailContext(auth, options);
            messages = new MessagesContext(auth, options);
            statistics = new StatisticsContext(auth, options);
        }

        IClassicEmail ITransactional.ClassicEmail
        {
            get { return classicEmail; }
        }

        IAgencyClassicEmail IAgencyTransactional.ClassicEmail
        {
            get { return classicEmail; }
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
            return new MessageBuilder(smartEmail, classicEmail);
        }

        IAgencyMessageBuilder IAgencyTransactional.MessageBuilder()
        {
            return new MessageBuilder(smartEmail, classicEmail);
        }

        public RateLimited<Statistics> Statistics(Guid? smartEmailId = null, string @group = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client)
        {
            return statistics.Statistics(smartEmailId, @group, from, to, timezone);
        }

        public RateLimited<Statistics> Statistics(string clientId, Guid? smartEmailId = null, string @group = null, DateTime? from = null, DateTime? to = null, DisplayedTimeZone timezone = DisplayedTimeZone.Client)
        {
            return statistics.Statistics(clientId, smartEmailId, @group, from, to, timezone);
        }
    }
}
