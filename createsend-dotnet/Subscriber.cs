using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    public class Subscriber
    {
        private string _listID;

        public Subscriber(string listID)
        {
            _listID = listID;
        }

        public SubscriberDetail Get(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            string json = HttpHelper.Get(string.Format("/subscribers/{0}.json", _listID), queryArguments);
            return JavaScriptConvert.DeserializeObject<SubscriberDetail>(json);
        }

        public string Add(string emailAddress, string name, List<SubscriberCustomField> customFields, bool resubscribe)
        {
            string json = HttpHelper.Post(string.Format("/subscribers/{0}.json", _listID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "EmailAddress", emailAddress }, { "Name", name }, { "CustomFields", customFields }, { "Resubscribe", resubscribe } }
                ));
            return JavaScriptConvert.DeserializeObject<string>(json);
        }
        
        public void Import(List<SubscriberDetail> subscribers, bool resubscribe)
        {
            Dictionary<string, object> reworkedSusbcribers = new Dictionary<string, object>();
            foreach (SubscriberDetail subscriber in subscribers)
            {
                Dictionary<string, object> subscriberWithoutDate = new Dictionary<string, object>() { { "EmailAddress", subscriber.EmailAddress }, { "Name", subscriber.Name }, { "CustomFields", subscriber.CustomFields } };
                reworkedSusbcribers.Add("Subscriber", subscriberWithoutDate);
            }

            try
            {
                string json = HttpHelper.Post(string.Format("/subscribers/{0}/import.json", _listID), null, JavaScriptConvert.SerializeObject(
                    new Dictionary<string, object>() { { "Subscribers", reworkedSusbcribers }, { "Resubscribe", resubscribe } }
                    ));
            }
            catch (CreatesendException ex)
            {
                //TODO : return/process whatever this requests extra result data is, of type T
                /*
                 * ErrorResult<T> = JavaScriptConvert.DeserializeObject<ErrorResult<T>>(ex.ResponseData);
                 * */
                throw ex;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Unusbscribe(string emailAddress)
        {
            return (HttpHelper.Post(string.Format("/subscribers/{0}/unsubscribe.json", _listID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, string>() { {"EmailAddress", emailAddress } }
                )) != null);
        }
    }
}
