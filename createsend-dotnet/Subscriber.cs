﻿using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Subscriber : CreateSendBase
    {
        public Subscriber(AuthenticationDetails auth, string listID)
            : base(auth)
        {
            ListID = listID;
        }

        public string ListID { get; set; }

        public SubscriberDetail Get(string emailAddress, bool includeTrackingPreference)
        {
            NameValueCollection queryArguments = new NameValueCollection
            {
                { "email", emailAddress },
                { "includeTrackingPreference", includeTrackingPreference.ToString() }
            };

            return HttpGet<SubscriberDetail>(
                string.Format("/subscribers/{0}.json", ListID), queryArguments);
        }

        public IEnumerable<HistoryItem> GetHistory(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            return HttpGet<IEnumerable<HistoryItem>>(
                string.Format("/subscribers/{0}/history.json", ListID),
                queryArguments);
        }

        public string Add(string emailAddress, string name,
            List<SubscriberCustomField> customFields, bool resubscribe,
            ConsentToTrack consentToTrack, string mobileNumber = null, ConsentToSendSms? consentToSendSms = null)
        {
            return Add(emailAddress, name, customFields, resubscribe, false, consentToTrack, mobileNumber, consentToSendSms);
        }

        public string Add(string emailAddress, string name,
            List<SubscriberCustomField> customFields, bool resubscribe,
            bool restartSubscriptionBasedAutoresponders, ConsentToTrack consentToTrack,
            string mobileNumber = null, ConsentToSendSms? consentToSendSms = null)
        {
            return HttpPost<Dictionary<string, object>, string>(
                string.Format("/subscribers/{0}.json", ListID), null,
                new Dictionary<string, object>()
                {
                    { "EmailAddress", emailAddress },
                    { "Name", name },
                    { "MobileNumber" , mobileNumber},
                    { "CustomFields", customFields },
                    { "Resubscribe", resubscribe },
                    { "RestartSubscriptionBasedAutoresponders", restartSubscriptionBasedAutoresponders },
                    { "ConsentToTrack", consentToTrack },
                    { "ConsentToSendSms", consentToSendSms }
                });
        }

        public void Update(string emailAddress, string newEmailAddress,
            string name, List<SubscriberCustomField> customFields,
            bool resubscribe, ConsentToTrack consentToTrack, string mobileNumber = null, ConsentToSendSms? consentToSendSms = null)
        {
            Update(emailAddress, newEmailAddress, name, customFields,
                resubscribe, false, consentToTrack, mobileNumber, consentToSendSms);
        }

        public void Update(string emailAddress, string newEmailAddress,
            string name, List<SubscriberCustomField> customFields, bool resubscribe,
            bool restartSubscriptionBasedAutoresponders, ConsentToTrack consentToTrack,
            string mobileNumber = null, ConsentToSendSms? consentToSendSms = null)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            HttpPut<Dictionary<string, object>, string>(
                string.Format("/subscribers/{0}.json", ListID), queryArguments,
                new Dictionary<string, object>()
                {
                    { "EmailAddress", newEmailAddress },
                    { "Name", name },
                    { "MobileNumber" , mobileNumber},
                    { "CustomFields", customFields },
                    { "Resubscribe", resubscribe },
                    { "RestartSubscriptionBasedAutoresponders", restartSubscriptionBasedAutoresponders },
                    { "ConsentToTrack", consentToTrack },
                    { "ConsentToSendSms", consentToSendSms }
                });
        }

        public void Delete(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);
            HttpDelete(string.Format("/subscribers/{0}.json", ListID), queryArguments);
        }

        public BulkImportResults Import(List<SubscriberDetail> subscribers, bool resubscribe)
        {
            return Import(subscribers, resubscribe, false);
        }

        public BulkImportResults Import(List<SubscriberDetail> subscribers,
            bool resubscribe, bool queueSubscriptionBasedAutoResponders)
        {
            return Import(subscribers, resubscribe,
                queueSubscriptionBasedAutoResponders, false);
        }

        public BulkImportResults Import(List<SubscriberDetail> subscribers,
            bool resubscribe, bool queueSubscriptionBasedAutoResponders,
            bool restartSubscriptionBasedAutoresponders)
        {
            List<object> reworkedSubscribers = new List<object>();
            foreach (SubscriberDetail subscriber in subscribers)
            {
                Dictionary<string, object> subscriberWithoutDate =
                    new Dictionary<string, object>()
                {
                    { "EmailAddress", subscriber.EmailAddress },
                    { "Name", subscriber.Name },
                    { "CustomFields", subscriber.CustomFields },
                    { "ConsentToTrack", subscriber.ConsentToTrack },
                    { "MobileNumber", subscriber.MobileNumber },
                    { "ConsentToSendSms", subscriber.ConsentToSendSms }
                };

                reworkedSubscribers.Add(subscriberWithoutDate);
            }

            return HttpPost<Dictionary<string, object>, BulkImportResults,
                ErrorResult<BulkImportResults>>(
                string.Format("/subscribers/{0}/import.json", ListID), null,
                new Dictionary<string, object>()
                {
                    { "Subscribers", reworkedSubscribers },
                    { "Resubscribe", resubscribe },
                    { "QueueSubscriptionBasedAutoResponders", queueSubscriptionBasedAutoResponders },
                    { "RestartSubscriptionBasedAutoresponders", restartSubscriptionBasedAutoresponders }
                });
        }

        public bool Unsubscribe(string emailAddress)
        {
            string result = HttpPost<Dictionary<string, string>, string>(
                string.Format("/subscribers/{0}/unsubscribe.json", ListID), null,
                new Dictionary<string, string>()
                {
                    { "EmailAddress", emailAddress }
                });

            return result != null;
        }
    }
}
