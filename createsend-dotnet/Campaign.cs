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
            string json = HttpHelper.Post(string.Format("/campaigns/{0}.json", clientID), null, JsonConvert.SerializeObject(
                new Dictionary<string, object>() { { "Subject", subject }, { "Name", name }, { "FromName", fromName}, { "FromEmail", fromEmail }, { "ReplyTo", replyTo }, { "HtmlUrl", htmlUrl }, { "TextUrl", textUrl }, { "ListIDs", listIDs }, { "SegmentIDs", segmentIDs } })
                );
            return JsonConvert.DeserializeObject<string>(json);
        }

        public void SendPreview(List<string> recipients, string personalize)
        {
            string json = HttpHelper.Post(string.Format("/campaigns/{0}/sendpreview.json", CampaignID), null, JsonConvert.SerializeObject(
                new Dictionary<string, object>() { { "PreviewRecipients", recipients}, { "Personalize", personalize} })
                );
        }

        public void Send(string confirmationEmail, DateTime sendDate)
        {
            string json = HttpHelper.Post(string.Format("/campaigns/{0}/send.json", CampaignID), null, JsonConvert.SerializeObject(
                new Dictionary<string, object>() { { "ConfirmationEmail", confirmationEmail }, { "SendDate", sendDate.ToString("yyyy-MM-dd HH:mm:ss") } })
                );
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/campaigns/{0}.json", CampaignID), null);
        }

        public CampaignSummary Summary()
        {
            string json = HttpHelper.Get(string.Format("/campaigns/{0}/summary.json", CampaignID), null);
            return JsonConvert.DeserializeObject<CampaignSummary>(json);
        }

        public CampaignListsAndSegments ListsAndSegments()
        {
            string json = HttpHelper.Get(string.Format("/campaigns/{0}/listsandsegments.json", CampaignID), null);
            return JsonConvert.DeserializeObject<CampaignListsAndSegments>(json);
        }

        public PagedCollection<CampaignRecipient> Recipients(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/recipients.json", CampaignID), queryArguments);
            return JsonConvert.DeserializeObject<PagedCollection<CampaignRecipient>>(json);
        }

        public PagedCollection<CampaignOpenDetail> Opens(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/opens.json", CampaignID), queryArguments);
            return JsonConvert.DeserializeObject<PagedCollection<CampaignOpenDetail>>(json);
        }

        public PagedCollection<CampaignUnsubscribeDetail> Unsubscribes(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/unsubscribes.json", CampaignID), queryArguments);
            return JsonConvert.DeserializeObject<PagedCollection<CampaignUnsubscribeDetail>>(json);
        }

        public PagedCollection<CampaignClickDetail> Clicks(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/clicks.json", CampaignID), queryArguments);
            return JsonConvert.DeserializeObject<PagedCollection<CampaignClickDetail>>(json);
        }

        public PagedCollection<CampaignBounceDetail> Bounces(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/bounces.json", CampaignID), queryArguments);
            return JsonConvert.DeserializeObject<PagedCollection<CampaignBounceDetail>>(json);
        }        
    }
}
