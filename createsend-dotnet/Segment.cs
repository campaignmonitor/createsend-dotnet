using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Segment
    {
        private string _segmentID;

        public Segment(string segmentID)
        {
            _segmentID = segmentID;
        }

        public static string Create(string listID, string title, SegmentRules rules)
        {
            string json = "";
            try
            {
                json = HttpHelper.Post(string.Format("/segments/{0}.json", listID), null, JavaScriptConvert.SerializeObject(
                    new Dictionary<string, object>() { { "ListID", listID }, { "Title", title }, { "Rules", rules } })
                    );
            }
            catch (CreatesendException ex)
            {
                if (!ex.Data.Contains("ErrorResult") && ex.Data.Contains("ErrorResponse"))
                {
                    ErrorResult<RuleErrorResults> result = JavaScriptConvert.DeserializeObject<ErrorResult<RuleErrorResults>>(ex.Data["ErrorResponse"].ToString());
                    ex.Data.Add("ErrorResult", result);
                }

                throw ex;
            }
            catch (Exception ex) { throw ex; }
            
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public void Update(string title, SegmentRules rules)
        {
            HttpHelper.Put(string.Format("/segments/{0}.json", _segmentID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "Title", title }, { "Rules", rules } })
                );
        }

        public void AddRule(string subject, List<string> clauses)
        {
            HttpHelper.Post(string.Format("/segments/{0}/rules.json", _segmentID), null, JavaScriptConvert.SerializeObject(
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

            string json = HttpHelper.Get(string.Format("/segments/{0}/active.json", _segmentID), queryArguments);
            return JavaScriptConvert.DeserializeObject<PagedCollection<SubscriberDetail>>(json);
        }

        public SegmentDetail Details()
        {
            string json = HttpHelper.Get(string.Format("/segments/{0}.json", _segmentID), null);
            return JavaScriptConvert.DeserializeObject<SegmentDetail>(json);
        }

        public void ClearRules()
        {
            HttpHelper.Delete(string.Format("/segments/{0}/rules.json", _segmentID), null);
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/segments/{0}.json", _segmentID), null);
        }
    }
}
