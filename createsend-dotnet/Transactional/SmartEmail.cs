using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet.Transactional
{
    public interface ISmartEmail
    {
        RateLimited<RecipientStatus[]> Send(Guid smartEmailId, EmailAddress[] cc = null, EmailAddress[] bcc = null, Attachment[] attachments = null,
            IDictionary<string, object> data = null, bool addRecipientsToList = true, ConsentToTrack consentToTrack = ConsentToTrack.Unchanged, params EmailAddress[] to);
        RateLimited<SmartEmailDetail> Details(Guid smartEmailId);
        RateLimited<SmartEmailListDetail[]> List(SmartEmailListStatus status = SmartEmailListStatus.All);
    }

    public interface IAgencySmartEmail : ISmartEmail
    {
        RateLimited<SmartEmailListDetail[]> List(string clientId, SmartEmailListStatus status = SmartEmailListStatus.All);
    }

    internal class SmartEmailContext : CreateSendBase, ISmartEmail, IAgencySmartEmail
    {
        public SmartEmailContext(AuthenticationDetails authenticationDetails, ICreateSendOptions options)
            : base(authenticationDetails, options)
        {
        }

        public RateLimited<RecipientStatus[]> Send(Guid smartEmailId, EmailAddress[] cc = null, EmailAddress[] bcc = null, Attachment[] attachments = null,
            IDictionary<string, object> data = null, bool addRecipientsToList = true, ConsentToTrack consentToTrack = ConsentToTrack.Unchanged, params EmailAddress[] to)
        {
            return Send(smartEmailId, new SmartEmail(to, cc, bcc, attachments, data, addRecipientsToList, consentToTrack), new NameValueCollection());
        }

        private RateLimited<RecipientStatus[]> Send(Guid smartEmailId, SmartEmail payload, NameValueCollection query)
        {
            return HttpPost<SmartEmail, RateLimited<RecipientStatus[]>>(string.Format("/transactional/smartemail/{0}/send", smartEmailId), query, payload);
        }

        public RateLimited<SmartEmailListDetail[]> List(SmartEmailListStatus status = SmartEmailListStatus.All)
        {
            return List(CreateQueryString(status));
        }

        public RateLimited<SmartEmailListDetail[]> List(string clientId, SmartEmailListStatus status = SmartEmailListStatus.All)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            return List(CreateQueryString(status, clientId));
        }

        private RateLimited<SmartEmailListDetail[]> List(NameValueCollection query)
        {
            return HttpGet<RateLimited<SmartEmailListDetail[]>>("/transactional/smartemail", query);
        }

        public RateLimited<SmartEmailDetail> Details(Guid smartEmailId)
        {
            return Details(smartEmailId, new NameValueCollection());
        }

        private RateLimited<SmartEmailDetail> Details(Guid smartEmailId, NameValueCollection query)
        {
            return HttpGet<RateLimited<SmartEmailDetail>>(string.Format("/transactional/smartemail/{0}", smartEmailId), query);
        }

        private NameValueCollection CreateQueryString(SmartEmailListStatus status, string clientId = null)
        {
            return this.CreateQueryString(
                clientId,
                query: new NameValueCollection
                    {
                        { "status", status.Encode() }
                    });
        }
    }

    internal class SmartEmail
    {
        public SmartEmail(EmailAddress[] to, EmailAddress[] cc, EmailAddress[] bcc, Attachment[] attachments,
            IDictionary<string, object> data, bool addRecipientsToList, ConsentToTrack consentToTrack)
        {
            To = to;
            CC = cc;
            BCC = bcc;
            Attachments = attachments;
            Data = data;
            AddRecipientsToList = addRecipientsToList;
            ConsentToTrack = consentToTrack;
        }

        public EmailAddress[] To { get; private set; }
        public EmailAddress[] CC { get; private set; }
        public EmailAddress[] BCC { get; private set; }
        public Attachment[] Attachments { get; private set; }
        public IDictionary<string, object> Data { get; private set; }
        public bool AddRecipientsToList { get; private set; }
        public ConsentToTrack ConsentToTrack { get; private set; }
    }
}
