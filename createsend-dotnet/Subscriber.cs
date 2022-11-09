using System.Collections.Generic;
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

        /// <summary>
        /// Get the details of a subscriber by their email address.
        /// </summary>
        /// <param name="emailAddress">The subscriber's email address</param>
        /// <param name="includeTrackingPreference">Should the results include the tracking preference?</param>
        /// <remarks>
        /// <para>This method does not yet support mobileNumber. Regardless of the subscriber's number, null will be returned.</para>
        /// </remarks>
        /// <returns>The subscriber detail that matches the given email address</returns>
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

        /// <summary>
        /// Add a subscriber with the provided details to the list without restarting subscription-based autoresponders.
        /// </summary>
        /// <param name="emailAddress">The subscriber's email address</param>
        /// <param name="name">The subscriber's name</param>
        /// <param name="customFields">Any custom fields that relate to the subscriber</param>
        /// <param name="resubscribe">Should the subscriber be resubscribed if inactive/suppressed?</param>
        /// <param name="consentToTrack">The subscriber's consent to track status</param>
        /// <param name="mobileNumber">(Optional) The subscriber's mobile number</param>
        /// <remarks>
        /// <para>Excluding the mobileNumber parameter, or passing null, will not alter the subscriber's mobile number.</para>
        /// <para>In order to remove a mobile number, pass in an empty string ("").</para>
        /// <para><see href="https://www.campaignmonitor.com/api/v3-3/subscribers/">Visit the API documentation for more info.</see></para>
        /// </remarks>
        /// <returns>The new subscriber's email address</returns>
        public string Add(string emailAddress, string name,
            List<SubscriberCustomField> customFields, bool resubscribe,
            ConsentToTrack consentToTrack, string mobileNumber = null)
        {
            return Add(emailAddress, name, customFields, resubscribe, false, consentToTrack, mobileNumber);
        }

        /// <summary>
        /// Add a subscriber with the provided details to the list.
        /// </summary>
        /// <param name="emailAddress">The subscriber's email address</param>
        /// <param name="name">The subscriber's name</param>
        /// <param name="customFields">Any custom fields that relate to the subscriber</param>
        /// <param name="resubscribe">Should the subscriber be resubscribed if inactive/suppressed?</param>
        /// <param name="restartSubscriptionBasedAutoresponders">Should resubscribed autoresponders restart their sequence?</param>
        /// <param name="consentToTrack">The subscriber's consent to track status</param>
        /// <param name="mobileNumber">(Optional) The subscriber's mobile number</param>
        /// <remarks>
        /// <para>Excluding the mobileNumber parameter, or passing null, will not alter the subscriber's mobile number.</para>
        /// <para>In order to remove a mobile number, pass in an empty string ("").</para>
        /// <para><see href="https://www.campaignmonitor.com/api/v3-3/subscribers/">Visit the API documentation for more info.</see></para>
        /// </remarks>
        /// <returns>The new subscriber's email address</returns>
        public string Add(string emailAddress, string name,
            List<SubscriberCustomField> customFields, bool resubscribe,
            bool restartSubscriptionBasedAutoresponders, ConsentToTrack consentToTrack, string mobileNumber)
        {
            return HttpPost<Dictionary<string, object>, string>(
                string.Format("/subscribers/{0}.json", ListID), null,
                new Dictionary<string, object>()
                {
                    { "EmailAddress", emailAddress },
                    { "Name", name },
                    { "CustomFields", customFields },
                    { "Resubscribe", resubscribe },
                    { "RestartSubscriptionBasedAutoresponders", restartSubscriptionBasedAutoresponders },
                    { "ConsentToTrack", consentToTrack },
                    { "MobileNumber", mobileNumber }
                });
        }

        /// <summary>
        /// Update the details of a subscriber by their email address without restarting subscription-based autoresponders.
        /// </summary>
        /// <param name="emailAddress">The subscriber's email address</param>
        /// <param name="name">The subscriber's name</param>
        /// <param name="customFields">Any custom fields that relate to the subscriber</param>
        /// <param name="resubscribe">Should the subscriber be resubscribed if inactive/suppressed?</param>
        /// <param name="consentToTrack">The subscriber's consent to track status</param>
        /// <param name="mobileNumber">(Optional) The subscriber's mobile number</param>
        /// <remarks>
        /// <para>Excluding the mobileNumber parameter, or passing null, will not alter the subscriber's mobile number.</para>
        /// <para>In order to remove a mobile number, pass in an empty string ("").</para>
        /// <para><see href="https://www.campaignmonitor.com/api/v3-3/subscribers/">Visit the API documentation for more info.</see></para>
        /// </remarks>
        public void Update(string emailAddress, string newEmailAddress,
            string name, List<SubscriberCustomField> customFields,
            bool resubscribe, ConsentToTrack consentToTrack, string mobileNumber = null)
        {
            Update(emailAddress, newEmailAddress, name, customFields,
                resubscribe, false, consentToTrack, mobileNumber);
        }

        /// <summary>
        /// Update the details of a subscriber by their email address.
        /// </summary>
        /// <param name="emailAddress">The subscriber's current email address</param>
        /// <param name="newEmailAddress">The subscriber's new email address</param>
        /// <param name="name">The subscriber's name</param>
        /// <param name="customFields">Any custom fields that relate to the subscriber</param>
        /// <param name="resubscribe">Should the subscriber be resubscribed if inactive/suppressed?</param>
        /// <param name="restartSubscriptionBasedAutoresponders">Should resubscribed autoresponders restart their sequence?</param>
        /// <param name="consentToTrack">The subscriber's consent to track status</param>
        /// <param name="mobileNumber">(Optional) The subscriber's mobile number</param>
        /// <remarks>
        /// <para>Excluding the mobileNumber parameter, or passing null, will not alter the subscriber's mobile number.</para>
        /// <para>In order to remove a mobile number, pass in an empty string ("").</para>
        /// <para><see href="https://www.campaignmonitor.com/api/v3-3/subscribers/">Visit the API documentation for more info.</see></para>
        /// </remarks>
        public void Update(string emailAddress, string newEmailAddress,
            string name, List<SubscriberCustomField> customFields, bool resubscribe,
            bool restartSubscriptionBasedAutoresponders, ConsentToTrack consentToTrack, string mobileNumber = null)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);

            HttpPut<Dictionary<string, object>, string>(
                string.Format("/subscribers/{0}.json", ListID), queryArguments,
                new Dictionary<string, object>()
                {
                    { "EmailAddress", newEmailAddress },
                    { "Name", name },
                    { "CustomFields", customFields },
                    { "Resubscribe", resubscribe },
                    { "RestartSubscriptionBasedAutoresponders", restartSubscriptionBasedAutoresponders },
                    { "ConsentToTrack", consentToTrack },
                    { "MobileNumber", mobileNumber }
                });
        }

        public void Delete(string emailAddress)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("email", emailAddress);
            HttpDelete(string.Format("/subscribers/{0}.json", ListID), queryArguments);
        }

        /// <summary>
        /// Bulk import multiple subscribers into the list without queueing or restarting subscription-based autoresponders.
        /// </summary>
        /// <param name="subscribers">A list of subscribers to import</param>
        /// <param name="resubscribe">Should the subscribers be resubscribed if inactive/suppressed?</param>
        /// <returns>A class containing the results of the import.</returns>
        /// <remarks>
        /// <para>Excluding the mobileNumber parameter, or passing null, will not alter the subscriber's mobile number.</para>
        /// <para>In order to remove a mobile number, pass in an empty string ("").</para>
        /// <para><see href="https://www.campaignmonitor.com/api/v3-3/subscribers/">Visit the API documentation for more info.</see></para>
        /// </remarks>
        public BulkImportResults Import(List<SubscriberDetail> subscribers, bool resubscribe)
        {
            return Import(subscribers, resubscribe, false);
        }

        /// <summary>
        /// Bulk import multiple subscribers into the list without restarting subscription-based autoresponders.
        /// </summary>
        /// <param name="subscribers">A list of subscribers to import</param>
        /// <param name="resubscribe">Should the subscribers be resubscribed if inactive/suppressed?</param>
        /// <param name="queueSubscriptionBasedAutoResponders">Should automated workflow emails be queued for these subscribers?</param>
        /// <returns>A class containing the results of the import.</returns>
        /// <remarks>
        /// <para>Excluding the mobileNumber parameter, or passing null, will not alter the subscriber's mobile number.</para>
        /// <para>In order to remove a mobile number, pass in an empty string ("").</para>
        /// <para><see href="https://www.campaignmonitor.com/api/v3-3/subscribers/">Visit the API documentation for more info.</see></para>
        /// </remarks>
        public BulkImportResults Import(List<SubscriberDetail> subscribers,
            bool resubscribe, bool queueSubscriptionBasedAutoResponders)
        {
            return Import(subscribers, resubscribe,
                queueSubscriptionBasedAutoResponders, false);
        }

        /// <summary>
        /// Bulk import multiple subscribers into the list.
        /// </summary>
        /// <param name="subscribers">A list of subscribers to import</param>
        /// <param name="resubscribe">Should the subscribers be resubscribed if inactive/suppressed?</param>
        /// <param name="queueSubscriptionBasedAutoResponders">Should automated workflow emails be queued for these subscribers?</param>
        /// <param name="restartSubscriptionBasedAutoresponders">Should resubscribed autoresponders restart their sequence?</param>
        /// <returns>A class containing the results of the import.</returns>
        /// <remarks>
        /// <para>Excluding the mobileNumber parameter, or passing null, will not alter the subscriber's mobile number.</para>
        /// <para>In order to remove a mobile number, pass in an empty string ("").</para>
        /// <para><see href="https://www.campaignmonitor.com/api/v3-3/subscribers/">Visit the API documentation for more info.</see></para>
        /// </remarks>
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
                    { "MobileNumber", subscriber.MobileNumber }
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
