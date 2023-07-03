using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

namespace createsend_dotnet
{
    public class Segment : CreateSendBase
    {
        public string SegmentID { get; set; }

        public Segment(AuthenticationDetails auth, string segmentID)
            : base(auth)
        {
            SegmentID = segmentID;
        }

        public static string Create(
            AuthenticationDetails auth, string listID, string title, SegmentRuleGroups ruleGroups)
        {
            return HttpHelper.Post<Dictionary<string, object>, string, ErrorResult<RuleErrorResults>>(
                auth, string.Format("/segments/{0}.json", listID), null,
                new Dictionary<string, object>() 
                { 
                    { "ListID", listID }, 
                    { "Title", title }, 
                    { "RuleGroups", ruleGroups }
                });
        }

        public void Update(string title, SegmentRuleGroups ruleGroups)
        {
            HttpPut<Dictionary<string, object>, string>(
                string.Format("/segments/{0}.json", SegmentID), null,
                new Dictionary<string, object>() 
                { 
                    { "Title", title }, 
                    { "RuleGroups", ruleGroups } 
                });
        }

        public void AddRuleGroup(SegmentRuleGroup ruleGroup)
        {
            HttpPost<Dictionary<string, object>, string>(
                string.Format("/segments/{0}/rules.json", SegmentID), null,
                new Dictionary<string, object>() 
                { 
                    { "Rules", ruleGroup.Rules } 
                });
        }

        public PagedCollection<SubscriberDetail> Subscribers()
        {
            return Subscribers(1, 1000, "email", "asc");
        }

        public PagedCollection<SubscriberDetail> Subscribers(
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Subscribers("", page, pageSize, orderField,
                orderDirection);
        }

        public PagedCollection<SubscriberDetail> Subscribers(
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return Subscribers(fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                page, pageSize, orderField, orderDirection);
        }

        private PagedCollection<SubscriberDetail> Subscribers(
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

            return HttpGet<PagedCollection<SubscriberDetail>>(
                string.Format("/segments/{0}/active.json", SegmentID), queryArguments);
        }

        public SegmentDetail Details()
        {
            return HttpGet<SegmentDetail>(
                string.Format("/segments/{0}.json", SegmentID), null);
        }

        public void ClearRules()
        {
            HttpDelete(
                string.Format("/segments/{0}/rules.json", SegmentID), null);
        }

        public void Delete()
        {
            HttpDelete(string.Format("/segments/{0}.json", SegmentID), null);
        }
    }
}
