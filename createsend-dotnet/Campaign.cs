using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Campaign
    {
        public string ApiKey { get; set; }

        private CreateSendCredentials AuthCredentials
        {
            get { return new CreateSendCredentials(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        public string CampaignID { get; set; }

        public Campaign(string campaignID)
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
        public static string Create(string apiKey, string clientID,
            string subject, string name, string fromName, string fromEmail,
            string replyTo, string htmlUrl, string textUrl, List<string>
            listIDs, List<string> segmentIDs)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                new CreateSendCredentials(apiKey, "x"), 
                string.Format("/campaigns/{0}.json", clientID), null, 
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

        /// <summary>
        /// Creates a campaign using the campaign details provided.
        /// </summary>
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
        public static string Create(string clientID, string subject,
            string name, string fromName, string fromEmail, string replyTo,
            string htmlUrl, string textUrl, List<string> listIDs,
            List<string> segmentIDs)
        {
            return Create(CreateSendOptions.ApiKey, clientID, subject, name,
                fromName, fromEmail, replyTo, htmlUrl, textUrl, listIDs,
                segmentIDs);
        }

        public static string CreateFromTemplate(string apiKey, string clientID,
            string subject, string name, string fromName, string fromEmail,
            string replyTo, List<string> listIDs, List<string> segmentIDs,
            string templateID, TemplateContent templateContent)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                new CreateSendCredentials(apiKey, "x"),
                string.Format("/campaigns/{0}/fromtemplate.json", clientID), null,
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

        public static string CreateFromTemplate(string clientID, string subject,
            string name, string fromName, string fromEmail, string replyTo,
            List<string> listIDs, List<string> segmentIDs, string templateID,
            TemplateContent templateContent)
        {
            return CreateFromTemplate(CreateSendOptions.ApiKey, clientID, subject, name,
                fromName, fromEmail, replyTo, listIDs, segmentIDs, templateID,
                templateContent);
        }

        public void SendPreview(List<string> recipients, string personalize)
        {
            HttpHelper.Post<Dictionary<string, object>, string>(
                AuthCredentials, 
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
            Send(confirmationEmail, sendDate.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void Send(string confirmationEmail, string sendDate)
        {
            HttpHelper.Post<Dictionary<string, object>, string>(
                AuthCredentials,
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
            HttpHelper.Post<Dictionary<string, object>, string>(
                AuthCredentials, 
                string.Format("/campaigns/{0}/unschedule.json", CampaignID),
                null, null);
        }

        public void Delete()
        {
            HttpHelper.Delete(AuthCredentials, string.Format("/campaigns/{0}.json", CampaignID), null);
        }

        public CampaignSummary Summary()
        {
            return HttpHelper.Get<CampaignSummary>(AuthCredentials, string.Format("/campaigns/{0}/summary.json", CampaignID), null);
        }

        public IEnumerable<EmailClient> EmailClientUsage()
        {
            return HttpHelper.Get<IEnumerable<EmailClient>>(AuthCredentials,
                string.Format("/campaigns/{0}/emailclientusage.json",
                CampaignID), null);
        }

        public CampaignListsAndSegments ListsAndSegments()
        {
            return HttpHelper.Get<CampaignListsAndSegments>(AuthCredentials, string.Format("/campaigns/{0}/listsandsegments.json", CampaignID), null);
        }

        public PagedCollection<CampaignRecipient> Recipients(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignRecipient>>(AuthCredentials, string.Format("/campaigns/{0}/recipients.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignOpenDetail> Opens(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignOpenDetail>>(AuthCredentials, string.Format("/campaigns/{0}/opens.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignUnsubscribeDetail> Unsubscribes(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignUnsubscribeDetail>>(AuthCredentials, string.Format("/campaigns/{0}/unsubscribes.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignSpamComplaint> SpamComplaints(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignSpamComplaint>>(AuthCredentials, string.Format("/campaigns/{0}/spam.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignClickDetail> Clicks(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignClickDetail>>(AuthCredentials, string.Format("/campaigns/{0}/clicks.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignBounceDetail> Bounces(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignBounceDetail>>(AuthCredentials, string.Format("/campaigns/{0}/bounces.json", CampaignID), queryArguments);
        }        
    }
}
