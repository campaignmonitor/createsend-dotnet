using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Subscriber
    {
        public string ApiKey { get; set; }

        private NetworkCredential AuthCredentials
        {
            get { return new NetworkCredential(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        public string ListID { get; set; }

        public Subscriber(string listID)
        {
            ListID = listID;
        }

        public SubscriberDetail Get(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            return HttpHelper.Get<SubscriberDetail>(AuthCredentials, string.Format("/subscribers/{0}.json", ListID), queryArguments);
        }

        public IEnumerable<HistoryItem> GetHistory(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            return HttpHelper.Get<IEnumerable<HistoryItem>>(AuthCredentials, string.Format("/subscribers/{0}/history.json", ListID), queryArguments);
        }

        public string Add(string emailAddress, string name, List<SubscriberCustomField> customFields, bool resubscribe)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                AuthCredentials, 
                string.Format("/subscribers/{0}.json", ListID), null,
                new Dictionary<string, object>() 
                { 
                    { "EmailAddress", emailAddress }, 
                    { "Name", name }, 
                    { "CustomFields", customFields }, 
                    { "Resubscribe", resubscribe } 
                });
        }

        public void Update(string emailAddress, string newEmailAddress, string name, List<SubscriberCustomField> customFields, bool resubscribe)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            HttpHelper.Put<Dictionary<string, object>, string>(
                AuthCredentials, 
                string.Format("/subscribers/{0}.json", ListID), queryArguments, 
                new Dictionary<string, object>() 
                { 
                    { "EmailAddress", newEmailAddress }, 
                    { "Name", name }, 
                    { "CustomFields", customFields }, 
                    { "Resubscribe", resubscribe } 
                });
        }

        public void Delete(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);
            HttpHelper.Delete(AuthCredentials, string.Format("/subscribers/{0}.json", ListID),
                              queryArguments);
        }
        
        public BulkImportResults Import(List<SubscriberDetail> subscribers, bool resubscribe)
        {
            return Import(subscribers, resubscribe, false);
        }

        public BulkImportResults Import(List<SubscriberDetail> subscribers, bool resubscribe, bool queueSubscriptionBasedAutoResponders)
        {
            List<object> reworkedSubscribers = new List<object>();
            foreach (SubscriberDetail subscriber in subscribers)
            {
                Dictionary<string, object> subscriberWithoutDate = new Dictionary<string, object>() 
                { 
                    { "EmailAddress", subscriber.EmailAddress }, 
                    { "Name", subscriber.Name }, 
                    { "CustomFields", subscriber.CustomFields } 
                };
                reworkedSubscribers.Add(subscriberWithoutDate);
            }

            return HttpHelper.Post<Dictionary<string, object>, BulkImportResults, ErrorResult<BulkImportResults>>(
                AuthCredentials, 
                string.Format("/subscribers/{0}/import.json", ListID), null, 
                new Dictionary<string, object>() 
                { 
                    { "Subscribers", reworkedSubscribers }, 
                    { "Resubscribe", resubscribe },
                    { "QueueSubscriptionBasedAutoResponders", queueSubscriptionBasedAutoResponders }
                });
        }

        public bool Unsubscribe(string emailAddress)
        {
            string result = HttpHelper.Post<Dictionary<string, string>, string>(
                AuthCredentials, 
                string.Format("/subscribers/{0}/unsubscribe.json", ListID), null, 
                new Dictionary<string, string>() 
                { 
                    {"EmailAddress", emailAddress } 
                });

            return result != null;
        }
    }
}
