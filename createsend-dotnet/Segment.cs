using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Segment
    {
        public string SegmentID { get; set; }

        public Segment(string segmentID)
        {
            SegmentID = segmentID;
        }

        public static string Create(string listID, string title, SegmentRules rules)
        {
            string json = "";
            try
            {
                json = HttpHelper.Post(string.Format("/segments/{0}.json", listID), null, JsonConvert.SerializeObject(
                    new Dictionary<string, object>() { { "ListID", listID }, { "Title", title }, { "Rules", rules } })
                    );
            }
            catch (CreatesendException ex)
            {
                if (!ex.Data.Contains("ErrorResult") && ex.Data.Contains("ErrorResponse"))
                {
                    ErrorResult<RuleErrorResults> result = JsonConvert.DeserializeObject<ErrorResult<RuleErrorResults>>(ex.Data["ErrorResponse"].ToString());
                    ex.Data.Add("ErrorResult", result);
                }

                throw ex;
            }
            catch (Exception ex) { throw ex; }
            
            return JsonConvert.DeserializeObject<string>(json);
        }

        public void Update(string title, SegmentRules rules)
        {
            HttpHelper.Put(string.Format("/segments/{0}.json", SegmentID), null, JsonConvert.SerializeObject(
                new Dictionary<string, object>() { { "Title", title }, { "Rules", rules } })
                );
        }

        public void AddRule(string subject, List<string> clauses)
        {
            HttpHelper.Post(string.Format("/segments/{0}/rules.json", SegmentID), null, JsonConvert.SerializeObject(
                    new Dictionary<string, object>() { { "Subject", subject }, { "Clauses", clauses } })
                    );
        }

        public PagedCollection<SubscriberDetail> Subscribers(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/segments/{0}/active.json", SegmentID), queryArguments);
            return JsonConvert.DeserializeObject<PagedCollection<SubscriberDetail>>(json);
        }

        public SegmentDetail Details()
        {
            string json = HttpHelper.Get(string.Format("/segments/{0}.json", SegmentID), null);
            return JsonConvert.DeserializeObject<SegmentDetail>(json);
        }

        public void ClearRules()
        {
            HttpHelper.Delete(string.Format("/segments/{0}/rules.json", SegmentID), null);
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/segments/{0}.json", SegmentID), null);
        }
    }
}
