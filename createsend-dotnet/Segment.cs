using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

namespace createsend_dotnet
{
    public class Segment : CreateSendBase
    {
        public Segment(AuthenticationDetails auth, string segmentID)
            : base(auth)
        {
            SegmentID = segmentID;
        }

        public string SegmentID { get; set; }

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

        public PagedCollection<SubscriberDetail> Subscribers(
            int page = 1,
            int pageSize = 1000,
            string orderField = "email",
            string orderDirection = "asc",
            bool includeTrackingPreference = false)
        {
            return Subscribers("", page, pageSize, orderField,
                orderDirection, includeTrackingPreference);
        }

        public PagedCollection<SubscriberDetail> Subscribers(
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection,
            bool includeTrackingPreference)
        {
            return Subscribers(fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                page, pageSize, orderField, orderDirection, includeTrackingPreference);
        }

        private PagedCollection<SubscriberDetail> Subscribers(
            string fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection,
            bool includeTrackingPreference)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate);
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);
            queryArguments.Add("includeTrackingPreference", includeTrackingPreference.ToString());

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
