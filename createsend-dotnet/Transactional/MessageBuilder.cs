using System;
using System.Collections.Generic;

namespace createsend_dotnet.Transactional
{
    public interface IMessageBuilder
    {
        IMessageBuilder From(EmailAddress from);
        IMessageBuilder ReplyTo(EmailAddress replyTo);
        IMessageBuilder Subject(string subject);
        IMessageBuilder To(EmailAddress to);
        IMessageBuilder To(EmailAddress[] to);
        IMessageBuilder CC(EmailAddress cc);
        IMessageBuilder CC(EmailAddress[] cc);
        IMessageBuilder BCC(EmailAddress bcc);
        IMessageBuilder BCC(EmailAddress[] bcc);
        IMessageBuilder Text(string text);
        IMessageBuilder Html(string html);
        IMessageBuilder Attachment(Attachment attachment);
        IMessageBuilder Attachment(Attachment[] attachments);
        IMessageBuilder Image(Image image);
        IMessageBuilder Image(Image[] images);
        IMessageBuilder Data(IDictionary<string, object> data);
        IMessageBuilder TrackOpens(bool trackOpens = true);
        IMessageBuilder TrackClicks(bool trackClicks = true);
        IMessageBuilder InlineCss(bool inlineCss = true);
        IMessageBuilder BasicGroup(string basicGroup);
        IMessageBuilder AddRecipientsToList(bool addRecipientsToList);
        IMessageBuilder AddRecipientsToListId(string addRecipientsToListId);
        
        RateLimited<RecipientStatus[]> Send();
        RateLimited<RecipientStatus[]> Send(Guid smartEmailId);
    }

    public interface IAgencyMessageBuilder : IMessageBuilder
    {
        RateLimited<RecipientStatus[]> Send(string clientId);
        RateLimited<RecipientStatus[]> Send(string clientId, Guid smartEmailId);
    }

    internal class MessageBuilder : IMessageBuilder, IAgencyMessageBuilder
    {
        private readonly SmartEmailContext smart;
        private readonly BasicEmailContext basic;
        private readonly List<EmailAddress> to;
        private readonly List<EmailAddress> cc;
        private readonly List<EmailAddress> bcc;
        private readonly List<Attachment> attachments;
        private readonly List<Image> images;
        private IDictionary<string, object> data;
        private string subject;
        private EmailAddress from;
        private EmailAddress replyTo;
        private string html;
        private string text;
        private bool trackOpens = true;
        private bool trackClicks = true;
        private bool inlineCss = true;
        private string basicGroup;
        private bool addRecipientsToList = true;
        private string listId;

        public MessageBuilder(SmartEmailContext smart, BasicEmailContext basic)
        {
            this.smart = smart;
            this.basic = basic;

            to = new List<EmailAddress>();
            cc = new List<EmailAddress>();
            bcc = new List<EmailAddress>();
            attachments = new List<Attachment>();
            images = new List<Image>();
        }

        public IMessageBuilder From(EmailAddress from)
        {
            if(from == null) throw new ArgumentNullException("from");

            this.from = from;
            return this;
        }

        public IMessageBuilder ReplyTo(EmailAddress replyTo)
        {
            if(replyTo == null) throw new ArgumentNullException("replyTo");

            this.replyTo = replyTo;
            return this;
        }

        public IMessageBuilder Subject(string subject)
        {
            if(subject == null) throw new ArgumentNullException("subject");

            this.subject = subject;
            return this;
        }

        public IMessageBuilder To(EmailAddress to)
        {
            if(to == null) throw new ArgumentNullException("to");

            this.to.Add(to);
            return this;
        }

        public IMessageBuilder To(EmailAddress[] to)
        {
            if(to == null) throw new ArgumentNullException("to");
            if(to.Length == 0) throw new ArgumentException("Cannot be empty", "to");

            this.to.AddRange(to);
            return this;
        }

        public IMessageBuilder CC(EmailAddress cc)
        {
            if(cc == null) throw new ArgumentNullException("cc");
            
            this.cc.Add(cc);
            return this;
        }

        public IMessageBuilder CC(EmailAddress[] cc)
        {
            if(cc == null) throw new ArgumentNullException("cc");
            if (cc.Length == 0) throw new ArgumentException("Cannot be empty", "cc");

            this.cc.AddRange(cc);
            return this;
        }

        public IMessageBuilder BCC(EmailAddress bcc)
        {
            if(bcc == null) throw new ArgumentNullException("bcc");

            this.bcc.Add(bcc);
            return this;
        }

        public IMessageBuilder BCC(EmailAddress[] bcc)
        {
            if (bcc == null) throw new ArgumentNullException("bcc");
            if (bcc.Length == 0) throw new ArgumentException("Cannot be empty", "bcc");
            this.bcc.AddRange(bcc);
            return this;
        }

        public IMessageBuilder Text(string text)
        {
            if(text == null) throw new ArgumentNullException("text");

            this.text = text;
            return this;
        }

        public IMessageBuilder Html(string html)
        {
            if(html == null) throw new ArgumentNullException("html");

            this.html = html;
            return this;
        }

        public IMessageBuilder Attachment(Attachment attachment)
        {
            if(attachment == null) throw new ArgumentNullException("attachment");

            this.attachments.Add(attachment);
            return this;
        }

        public IMessageBuilder Attachment(Attachment[] attachments)
        {
            if(attachments == null) throw new ArgumentNullException("attachments");
            if (attachments.Length == 0) throw new ArgumentException("Cannot be empty", "attachments");

            this.attachments.AddRange(attachments);
            return this;
        }

        public IMessageBuilder Image(Image image)
        {
            if(image == null) throw new ArgumentNullException("image");

            this.images.Add(image);
            return this;
        }

        public IMessageBuilder Image(Image[] images)
        {
            if(images == null) throw new ArgumentNullException("images");
            if(images.Length == 0) throw new ArgumentException("Cannot be empty", "images");

            this.images.AddRange(images);
            return this;
        }

        public IMessageBuilder Data(IDictionary<string, object> data)
        {
            if(data == null) throw new ArgumentNullException("data");

            this.data = data;
            return this;
        }

        public IMessageBuilder TrackOpens(bool trackOpens = true)
        {
            this.trackOpens = trackOpens;

            return this;
        }

        public IMessageBuilder TrackClicks(bool trackClicks = true)
        {
            this.trackClicks = trackClicks;

            return this;
        }

        public IMessageBuilder InlineCss(bool inlineCss = true)
        {
            this.inlineCss = inlineCss;

            return this;
        }

        public IMessageBuilder BasicGroup(string basicGroup)
        {
            if(basicGroup == null) throw new ArgumentNullException("basicGroup");

            this.basicGroup = basicGroup;
            return this;
        }

        public IMessageBuilder AddRecipientsToList(bool addRecipientsToList)
        {
            this.addRecipientsToList = addRecipientsToList;

            return this;
        }

        public IMessageBuilder AddRecipientsToListId(string listId)
        {
            this.listId = listId;

            return this;
        }

        public RateLimited<RecipientStatus[]> Send()
        {
            return basic.Send(from, subject, html, text, replyTo, cc.ToArray(), bcc.ToArray(), images.ToArray(),
                attachments.ToArray(), trackOpens, trackClicks, inlineCss, basicGroup, listId, to.ToArray());
        }

        public RateLimited<RecipientStatus[]> Send(string clientId)
        {
            return basic.Send(clientId, from, subject, html, text, replyTo, cc.ToArray(), bcc.ToArray(), images.ToArray(),
                attachments.ToArray(), trackOpens, trackClicks, inlineCss, basicGroup, listId, to.ToArray());
        }


        public RateLimited<RecipientStatus[]> Send(Guid smartEmailId)
        {
            return smart.Send(
               smartEmailId,
               cc.ToArray(),
               bcc.ToArray(),
               attachments.ToArray(),
               data,
               addRecipientsToList,
               to.ToArray());
        }

        public RateLimited<RecipientStatus[]> Send(string clientId, Guid smartEmailId)
        {
            return smart.Send(
               clientId,
               smartEmailId,
               cc.ToArray(),
               bcc.ToArray(),
               attachments.ToArray(),
               data,
               addRecipientsToList,
               to.ToArray());
        }
    }
}
