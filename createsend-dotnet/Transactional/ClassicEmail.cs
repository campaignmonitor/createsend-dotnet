using System;
using System.Collections.Specialized;

namespace createsend_dotnet.Transactional
{
    public interface IClassicEmail
    {
        RateLimited<RecipientStatus[]> Send(EmailAddress from, string subject, string html, string text, ConsentToTrack consentToTrack, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null,
            Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string @group = null, string addRecipientsToListId = null, params EmailAddress[] to);
        RateLimited<ClassicEmailDetail[]> Groups();
    }

    public interface IAgencyClassicEmail : IClassicEmail
    {
        RateLimited<RecipientStatus[]> Send(string clientId, EmailAddress from, string subject, string html, string text, ConsentToTrack consentToTrack, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null,
            Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string @group = null, string addRecipientsToListId = null, params EmailAddress[] to);
        RateLimited<ClassicEmailDetail[]> Groups(string clientId);
    }

    internal class ClassicEmailContext : CreateSendBase, IClassicEmail, IAgencyClassicEmail
    {
        public ClassicEmailContext(AuthenticationDetails authenticationDetails, ICreateSendOptions options)
            : base(authenticationDetails, options)
        {
        }

        public RateLimited<RecipientStatus[]> Send(EmailAddress from, string subject, string html, string text, ConsentToTrack consentToTrack, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null, Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string @group = null, string addRecipientsToListId = null, params EmailAddress[] to)
        {
            return Send(new ClassicEmail(from, replyTo, to, cc, bcc, subject, html, text, images, attachments, trackOpens, trackClicks, inlineCss, @group, addRecipientsToListId, consentToTrack), this.CreateQueryString());
        }

        public RateLimited<RecipientStatus[]> Send(string clientId, EmailAddress from, string subject, string html, string text, ConsentToTrack consentToTrack, EmailAddress replyTo = null, EmailAddress[] cc = null, EmailAddress[] bcc = null, Image[] images = null, Attachment[] attachments = null, bool trackOpens = true, bool trackClicks = true, bool inlineCss = true, string @group = null, string addRecipientsToListId = null, params EmailAddress[] to)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            return Send(new ClassicEmail(from, replyTo, to, cc, bcc, subject, html, text, images, attachments, trackOpens, trackClicks, inlineCss, @group, addRecipientsToListId, consentToTrack), this.CreateQueryString(clientId));
        }

        private RateLimited<RecipientStatus[]> Send(ClassicEmail payload, NameValueCollection query)
        {
            return HttpPost<ClassicEmail, RateLimited<RecipientStatus[]>>("/transactional/classicemail/send", query, payload);
        }

        public RateLimited<ClassicEmailDetail[]> Groups()
        {
            return Groups(this.CreateQueryString());
        }

        public RateLimited<ClassicEmailDetail[]> Groups(string clientId)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            return Groups(this.CreateQueryString(clientId));
        }

        private RateLimited<ClassicEmailDetail[]> Groups(NameValueCollection query)
        {
            return HttpGet<RateLimited<ClassicEmailDetail[]>>("/transactional/classicemail/groups", query);
        }
    }

    internal class ClassicEmail
    {
        public ClassicEmail(EmailAddress from, EmailAddress replyTo, EmailAddress[] to, EmailAddress[] cc,
            EmailAddress[] bcc, string subject, string html, string text, Image[] images, Attachment[] attachments,
            bool trackOpens, bool trackClicks, bool inlineCss, string @group, string addRecipientsToListId, ConsentToTrack consentToTrack)
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
            Group = @group;
            AddRecipientsToListId = addRecipientsToListId;
            ConsentToTrack = consentToTrack;
        }

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
        public string Group { get; set; }
        public string AddRecipientsToListId { get; set; }
        public ConsentToTrack ConsentToTrack { get; set; }
    }
}
