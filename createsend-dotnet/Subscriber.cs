using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    public class Subscriber
    {
        public string ListID { get; set; }

        public Subscriber(string listID)
        {
            ListID = listID;
        }

        public SubscriberDetail Get(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            string json = HttpHelper.Get(string.Format("/subscribers/{0}.json", ListID), queryArguments);
            return JavaScriptConvert.DeserializeObject<SubscriberDetail>(json);
        }

        public string Add(string emailAddress, string name, List<SubscriberCustomField> customFields, bool resubscribe)
        {
            string json = HttpHelper.Post(string.Format("/subscribers/{0}.json", ListID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "EmailAddress", emailAddress }, { "Name", name }, { "CustomFields", customFields }, { "Resubscribe", resubscribe } }
                ));
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public void Update(string emailAddress, string newEmailAddress, string name, List<SubscriberCustomField> customFields, bool resubscribe)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            HttpHelper.Put(string.Format("/subscribers/{0}.json", ListID), queryArguments, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "EmailAddress", newEmailAddress }, { "Name", name }, { "CustomFields", customFields }, { "Resubscribe", resubscribe } }
                ));
        }
        
        public BulkImportResults Import(List<SubscriberDetail> subscribers, bool resubscribe)
        {
            List<object> reworkedSusbcribers = new List<object>();
            string json = "";
            foreach (SubscriberDetail subscriber in subscribers)
            {
                Dictionary<string, object> subscriberWithoutDate = new Dictionary<string, object>() { { "EmailAddress", subscriber.EmailAddress }, { "Name", subscriber.Name }, { "CustomFields", subscriber.CustomFields } };
                reworkedSusbcribers.Add(subscriberWithoutDate);
            }

            try
            {
                json = HttpHelper.Post(string.Format("/subscribers/{0}/import.json", ListID), null, JavaScriptConvert.SerializeObject(
                    new Dictionary<string, object>() { { "Subscribers", reworkedSusbcribers }, { "Resubscribe", resubscribe } }
                    ));
            }
            catch (CreatesendException ex)
            {
                if (!ex.Data.Contains("ErrorResult") && ex.Data.Contains("ErrorResponse"))
                {
                    ErrorResult<BulkImportResults> result = JavaScriptConvert.DeserializeObject<ErrorResult<BulkImportResults>>(ex.Data["ErrorResponse"].ToString());
                    ex.Data.Add("ErrorResult", result);
                }
                else if(ex.Data.Contains("ErrorResult"))
                {
                    ErrorResult<BulkImportResults> result = new ErrorResult<BulkImportResults>((ErrorResult)ex.Data["ErrorResult"]);
                    ex.Data["ErrorResult"] = result;
                }

                throw ex;
            }
            catch (Exception ex) { throw ex; }

            return JavaScriptConvert.DeserializeObject<BulkImportResults>(json);
        }

        public bool Unsubscribe(string emailAddress)
        {
            return (HttpHelper.Post(string.Format("/subscribers/{0}/unsubscribe.json", ListID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, string>() { {"EmailAddress", emailAddress } }
                )) != null);
        }
    }
}
