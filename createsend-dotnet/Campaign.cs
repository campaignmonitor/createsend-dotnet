using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Campaign
    {
        public string CampaignID { get; set; }

        public Campaign(string campaignID)
        {
            CampaignID = campaignID;
        }

        public static string Create(string clientID, string subject, string name, string fromName, string fromEmail, string replyTo, string htmlUrl, string textUrl, List<string> listIDs, List<string> segmentIDs)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
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

        public void SendPreview(List<string> recipients, string personalize)
        {
            HttpHelper.Post<Dictionary<string, object>, string>(
                string.Format("/campaigns/{0}/sendpreview.json", CampaignID), 
                null,                 
                new Dictionary<string, object>() 
                { 
                    { "PreviewRecipients", recipients}, 
                    { "Personalize", personalize} 
                });
        }

        public void Send(string confirmationEmail, DateTime sendDate)
        {
            HttpHelper.Post<Dictionary<string, object>, string>(
                string.Format("/campaigns/{0}/send.json", CampaignID), 
                null, 
                new Dictionary<string, object>()
                { 
                    { "ConfirmationEmail", confirmationEmail }, 
                    { "SendDate", sendDate.ToString("yyyy-MM-dd HH:mm:ss") } 
                });
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/campaigns/{0}.json", CampaignID), null);
        }

        public CampaignSummary Summary()
        {
            return HttpHelper.Get<CampaignSummary>(string.Format("/campaigns/{0}/summary.json", CampaignID), null);
        }

        public CampaignListsAndSegments ListsAndSegments()
        {
            return HttpHelper.Get<CampaignListsAndSegments>(string.Format("/campaigns/{0}/listsandsegments.json", CampaignID), null);
        }

        public PagedCollection<CampaignRecipient> Recipients(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignRecipient>>(string.Format("/campaigns/{0}/recipients.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignOpenDetail> Opens(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignOpenDetail>>(string.Format("/campaigns/{0}/opens.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignUnsubscribeDetail> Unsubscribes(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignUnsubscribeDetail>>(string.Format("/campaigns/{0}/unsubscribes.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignClickDetail> Clicks(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignClickDetail>>(string.Format("/campaigns/{0}/clicks.json", CampaignID), queryArguments);
        }

        public PagedCollection<CampaignBounceDetail> Bounces(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<CampaignBounceDetail>>(string.Format("/campaigns/{0}/bounces.json", CampaignID), queryArguments);
        }        
    }
}
