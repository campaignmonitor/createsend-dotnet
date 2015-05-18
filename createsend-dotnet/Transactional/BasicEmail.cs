using System;
using System.Collections.Specialized;

namespace createsend_dotnet.Transactional
{
    public interface IBasicEmail
    {
        RateLimited<RecipientStatus[]> Send(EmailAddress from, string subject, string html, string text, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null, Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string basicGroup = null, string addRecipientsToListId = null, params EmailAddress[] to);
        RateLimited<BasicEmailDetail[]> Groups();
    }

    public interface IAgencyBasicEmail : IBasicEmail
    {
        RateLimited<RecipientStatus[]> Send(string clientId, EmailAddress from, string subject, string html, string text, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null, Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string basicGroup = null, string addRecipientsToListId = null, params EmailAddress[] to);
        RateLimited<BasicEmailDetail[]> Groups(string clientId);
    }

    internal class BasicEmailContext : CreateSendBase, IBasicEmail, IAgencyBasicEmail
    {
        public BasicEmailContext(AuthenticationDetails authenticationDetails, ICreateSendOptions options)
            : base(authenticationDetails, options)
        {

        }

        public RateLimited<RecipientStatus[]> Send(EmailAddress from, string subject, string html, string text, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null, Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string basicGroup = null, string addRecipientsToListId = null, params EmailAddress[] to)
        {
            return Send(new BasicEmail(from, replyTo, to, cc, bcc, subject, html, text, images, attachments, trackOpens, trackClicks, inlineCss, basicGroup, addRecipientsToListId), this.CreateQueryString());
        }

        public RateLimited<RecipientStatus[]> Send(string clientId, EmailAddress from, string subject, string html, string text, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null, Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string basicGroup = null, string addRecipientsToListId = null, params EmailAddress[] to)
        {
            if (clientId == null) throw new ArgumentNullException("clientId");

            return Send(new BasicEmail(from, replyTo, to, cc, bcc, subject, html, text, images, attachments, trackOpens, trackClicks, inlineCss, basicGroup, addRecipientsToListId), this.CreateQueryString(clientId));
        }

        private RateLimited<RecipientStatus[]> Send(BasicEmail payload, NameValueCollection query)
        {
            return HttpPost<BasicEmail, RateLimited<RecipientStatus[]>>("/transactional/basicemail/send", query, payload);
        }

        public RateLimited<BasicEmailDetail[]> Groups()
        {
            return Groups(this.CreateQueryString());
        }

        public RateLimited<BasicEmailDetail[]> Groups(string clientId)
        {
            if (clientId == null) throw new ArgumentNullException("clientId");

            return Groups(this.CreateQueryString(clientId));
        }

        private RateLimited<BasicEmailDetail[]> Groups(NameValueCollection query)
        {
            return HttpGet<RateLimited<BasicEmailDetail[]>>("/transactional/basicemail/groups", query);
        }
    }

    internal class BasicEmail
    {
        public EmailAddress From { get; set; }
        public EmailAddress ReplyTo { get; set; }
        public EmailAddress[] To { get; set; }
        public EmailAddress[] CC { get; set; }
        public EmailAddress[] BCC { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public Image[] Images { get; set; }
        public Attachment[] Attachments { get; set; }
        public bool TrackOpens { get; set; }
        public bool TrackClicks { get; set; }
        public bool InlineCss { get; set; }
        public string BasicGroup { get; set; }
        public string AddRecipientsToListId { get; set; }

        public BasicEmail(EmailAddress from, EmailAddress replyTo, EmailAddress[] to, EmailAddress[] cc,
            EmailAddress[] bcc, string subject, string html, string text, Image[] images, Attachment[] attachments,
            bool trackOpens, bool trackClicks, bool inlineCss, string basicGroup, string addRecipientsToListId)
        {
            From = from;
            ReplyTo = replyTo;
            To = to;
            CC = cc;
            BCC = bcc;
            Subject = subject;
            Html = html;
            Text = text;
            Images = images;
            Attachments = attachments;
            TrackOpens = trackOpens;
            TrackClicks = trackClicks;
            InlineCss = inlineCss;
            BasicGroup = basicGroup;
            AddRecipientsToListId = addRecipientsToListId;
        }
    }
}
