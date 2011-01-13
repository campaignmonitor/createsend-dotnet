using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Campaign
    {
        private string _campaignID;

        public Campaign(string campaignID)
        {
            _campaignID = campaignID;
        }

        public static string Create(string clientID, string subject, string name, string fromName, string fromEmail, string replyTo, string htmlUrl, string textUrl, List<string> listIDs, List<string> segmentIDs)
        {
            string json = HttpHelper.Post(string.Format("/campaigns/{0}.json", clientID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "Subject", subject }, { "Name", name }, { "FromName", fromName}, { "FromEmail", fromEmail }, { "ReplyTo", replyTo }, { "HtmlUrl", htmlUrl }, { "TextUrl", textUrl }, { "ListIDs", listIDs }, { "SegmentIDs", segmentIDs } })
                );
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public void SendPreview(List<string> recipients, string personalize)
        {
            string json = HttpHelper.Post(string.Format("/campaigns/{0}/sendpreview.json", _campaignID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "PreviewRecipients", recipients}, { "Personalize", personalize} })
                );
        }

        public void Send(string confirmationEmail, DateTime sendDate)
        {
            string json = HttpHelper.Post(string.Format("/campaigns/{0}/send.json", _campaignID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "ConfirmationEmail", confirmationEmail }, { "SendDate", sendDate.ToString("yyyy-MM-dd HH:mm:ss") } })
                );
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/campaigns/{0}.json", _campaignID), null);
        }

        public CampaignSumamry Summary()
        {
            string json = HttpHelper.Get(string.Format("/campaigns/{0}/summary.json", _campaignID), null);
            return JavaScriptConvert.DeserializeObject<CampaignSumamry>(json);
        }

        public CampaignListsAndSegments ListsAndSegments()
        {
            string json = HttpHelper.Get(string.Format("/campaigns/{0}/listsandsegments.json", _campaignID), null);
            return JavaScriptConvert.DeserializeObject<CampaignListsAndSegments>(json);
        }

        public PagedCollection<CampaignRecipient> Recipients(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/recipients.json", _campaignID), queryArguments);
            return JavaScriptConvert.DeserializeObject<PagedCollection<CampaignRecipient>>(json);
        }

        public PagedCollection<CampaignOpenDetail> Opens(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/opens.json", _campaignID), queryArguments);
            return JavaScriptConvert.DeserializeObject<PagedCollection<CampaignOpenDetail>>(json);
        }

        public PagedCollection<CampaignUnsubscribeDetail> Unsubscribes(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/unsubscribes.json", _campaignID), queryArguments);
            return JavaScriptConvert.DeserializeObject<PagedCollection<CampaignUnsubscribeDetail>>(json);
        }

        public PagedCollection<CampaignClickDetail> Clicks(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/clicks.json", _campaignID), queryArguments);
            return JavaScriptConvert.DeserializeObject<PagedCollection<CampaignClickDetail>>(json);
        }

        public PagedCollection<CampaignBounceDetail> Bounces(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/campaigns/{0}/bounces.json", _campaignID), queryArguments);
            return JavaScriptConvert.DeserializeObject<PagedCollection<CampaignBounceDetail>>(json);
        }        
    }
}
