using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

namespace createsend_dotnet
{
    public class Campaign : CreateSendBase
    {
        public string CampaignID { get; set; }

        public Campaign(AuthenticationDetails auth, string campaignID)
            : base(auth)
        {
            CampaignID = campaignID;
        }

        /// <summary>
        /// Creates a campaign using the API key and campaign details provided.
        /// </summary>
        /// <param name="apiKey">API key to use</param>
        /// <param name="clientID">Client ID of the client for whom the
        /// campaign should be created</param>
        /// <param name="subject">A subject for the campaign</param>
        /// <param name="name">A name for the campaign</param>
        /// <param name="fromName">From name for the campaign</param>
        /// <param name="fromEmail">From email address for the campaign</param>
        /// <param name="replyTo">Reply-to address for the campaign</param>
        /// <param name="htmlUrl">URL for the HTML content for the 
        /// campaign</param>
        /// <param name="textUrl">URL for the text content for the campaign.
        /// Note that you may provide textUrl as null or an empty string and
        /// the text content for the campaign will be generated from the HTML
        /// content.</param>
        /// <param name="listIDs">IDs of the lists to which the campaign
        /// will be sent</param>
        /// <param name="segmentIDs">IDs of the segments to which the
        /// campaign will be sent</param>
        /// <returns>The ID of the newly created campaign</returns>
        public static string Create(AuthenticationDetails auth, string clientID,
            string subject, string name, string fromName, string fromEmail,
            string replyTo, string htmlUrl, string textUrl, List<string>
            listIDs, List<string> segmentIDs)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                auth, string.Format("/campaigns/{0}.json", clientID), null,
                new Dictionary<string, object>() 
                { 
                    { "Subject", subject }, 
                    { "Name", name }, 
                    { "FromName", fromName}, 
                    { "FromEmail", fromEmail }, 
                    { "ReplyTo", replyTo }, 
                    { "HtmlUrl", htmlUrl }, 
                    { "TextUrl", textUrl }, 
                    { "ListIDs", listIDs }, 
                    { "SegmentIDs", segmentIDs } 
                });
        }

        public static string CreateFromTemplate(AuthenticationDetails auth, string clientID,
            string subject, string name, string fromName, string fromEmail,
            string replyTo, List<string> listIDs, List<string> segmentIDs,
            string templateID, TemplateContent templateContent)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                auth, string.Format("/campaigns/{0}/fromtemplate.json", clientID), null,
                new Dictionary<string, object>()
                { 
                    { "Subject", subject }, 
                    { "Name", name }, 
                    { "FromName", fromName}, 
                    { "FromEmail", fromEmail }, 
                    { "ReplyTo", replyTo }, 
                    { "ListIDs", listIDs }, 
                    { "SegmentIDs", segmentIDs },
                    { "TemplateID", templateID },
                    { "TemplateContent", templateContent }
                });
        }

        public void SendPreview(List<string> recipients, string personalize)
        {
            HttpPost<Dictionary<string, object>, string>(
                string.Format("/campaigns/{0}/sendpreview.json", CampaignID), 
                null,                 
                new Dictionary<string, object>() 
                { 
                    { "PreviewRecipients", recipients}, 
                    { "Personalize", personalize} 
                });
        }

        public void Send(string confirmationEmail)
        {
            Send(confirmationEmail, "immediately");
        }

        public void Send(string confirmationEmail, DateTime sendDate)
        {
            Send(confirmationEmail, sendDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        }

        private void Send(string confirmationEmail, string sendDate)
        {
            HttpPost<Dictionary<string, object>, string>(
                string.Format("/campaigns/{0}/send.json", CampaignID),
                null,
                new Dictionary<string, object>()
                { 
                    { "ConfirmationEmail", confirmationEmail }, 
                    { "SendDate", sendDate } 
                });
        }

        public void Unschedule()
        {
            HttpPost<Dictionary<string, object>, string>(
                string.Format("/campaigns/{0}/unschedule.json", CampaignID),
                null, null);
        }

        public void Delete()
        {
            HttpDelete(string.Format("/campaigns/{0}.json", CampaignID), null);
        }

        public CampaignSummary Summary()
        {
            return HttpGet<CampaignSummary>(
                string.Format("/campaigns/{0}/summary.json", CampaignID), null);
        }

        public IEnumerable<EmailClient> EmailClientUsage()
        {
            return HttpGet<IEnumerable<EmailClient>>(
                string.Format("/campaigns/{0}/emailclientusage.json",
                CampaignID), null);
        }

        public CampaignListsAndSegments ListsAndSegments()
        {
            return HttpGet<CampaignListsAndSegments>(
                string.Format("/campaigns/{0}/listsandsegments.json", CampaignID), null);
        }

        public PagedCollection<CampaignRecipient> Recipients(
            int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<CampaignRecipient>>(
                string.Format("/campaigns/{0}/recipients.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignOpenDetail> Opens()
        {
            return Opens(1, 1000, "date", "asc");
        }

        public PagedCollection<CampaignOpenDetail> Opens(
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Opens("", page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<CampaignOpenDetail> Opens(
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Opens(fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), page,
                pageSize, orderField, orderDirection);
        }

        private PagedCollection<CampaignOpenDetail> Opens(
            string fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate);
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<CampaignOpenDetail>>(
                string.Format("/campaigns/{0}/opens.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignUnsubscribeDetail> Unsubscribes()
        {
            return Unsubscribes(1, 1000, "date", "asc");
        }

        public PagedCollection<CampaignUnsubscribeDetail> Unsubscribes(
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Unsubscribes("", page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<CampaignUnsubscribeDetail> Unsubscribes(
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Unsubscribes(fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                page, pageSize, orderField, orderDirection);
        }

        private PagedCollection<CampaignUnsubscribeDetail> Unsubscribes(
            string fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate);
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<CampaignUnsubscribeDetail>>(
                string.Format("/campaigns/{0}/unsubscribes.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignSpamComplaint> SpamComplaints()
        {
            return SpamComplaints(1, 1000, "date", "asc");
        }

        public PagedCollection<CampaignSpamComplaint> SpamComplaints(
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return SpamComplaints("", page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<CampaignSpamComplaint> SpamComplaints(
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return SpamComplaints(fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                page, pageSize, orderField, orderDirection);
        }

        private PagedCollection<CampaignSpamComplaint> SpamComplaints(
            string fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate);
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<CampaignSpamComplaint>>(
                string.Format("/campaigns/{0}/spam.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignClickDetail> Clicks()
        {
            return Clicks(1, 1000, "date", "asc");
        }

        public PagedCollection<CampaignClickDetail> Clicks(
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Clicks("", page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<CampaignClickDetail> Clicks(
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Clicks(fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                page, pageSize, orderField, orderDirection);
        }

        private PagedCollection<CampaignClickDetail> Clicks(
            string fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate);
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<CampaignClickDetail>>(
                string.Format("/campaigns/{0}/clicks.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignBounceDetail> Bounces()
        {
            return Bounces(1, 1000, "date", "asc");
        }

        public PagedCollection<CampaignBounceDetail> Bounces(
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Bounces("", page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<CampaignBounceDetail> Bounces(
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Bounces(fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                page, pageSize, orderField, orderDirection);
        }

        private PagedCollection<CampaignBounceDetail> Bounces(
            string fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate);
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<CampaignBounceDetail>>(
                string.Format("/campaigns/{0}/bounces.json", CampaignID), queryArguments);
        }        
    }
}
