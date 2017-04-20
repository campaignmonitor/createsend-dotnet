using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

namespace createsend_dotnet
{
    public class List : CreateSendBase
    {
        public List(AuthenticationDetails auth, string listID)
            : base(auth)
        {
            ListID = listID;
        }

        public string ListID { get; set; }

        public static string Create(
            AuthenticationDetails auth,
            string clientID,
            string title,
            string unsubscribePage,
            bool confirmedOptIn,
            string confirmationSuccessPage,
            UnsubscribeSetting unsubscribeSetting)
        {
            return HttpHelper.Post<ListDetail, string>(
                auth, string.Format("/lists/{0}.json", clientID), null,
                new ListDetail()
                {
                    Title = title,
                    UnsubscribePage = unsubscribePage,
                    ConfirmedOptIn = confirmedOptIn,
                    ConfirmationSuccessPage = confirmationSuccessPage,
                    UnsubscribeSetting = unsubscribeSetting.ToString()
                });
        }

        public void ActivateWebhook(string webhookID)
        {
            HttpPut<string, string>(
                string.Format("/lists/{0}/webhooks/{1}/activate.json",
                ListID, System.Net.WebUtility.HtmlEncode(webhookID)), null, null);
        }

        public PagedCollection<SubscriberDetail> Active()
        {
            return GenericPagedSubscriberGet("active", "", 1, 1000, "email", "asc");
        }

        public PagedCollection<SubscriberDetail> Active(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("active", fromDate, page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<SubscriberDetail> Bounced()
        {
            return GenericPagedSubscriberGet("bounced", "", 1, 1000, "email", "asc");
        }

        public PagedCollection<SubscriberDetail> Bounced(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("bounced", fromDate, page, pageSize, orderField, orderDirection);
        }

        public string CreateCustomField(
            string fieldName,
            CustomFieldDataType dataType,
            List<string> options)
        {
            return CreateCustomField(fieldName, dataType, options, true);
        }

        public string CreateCustomField(
            string fieldName,
            CustomFieldDataType dataType,
            List<string> options,
            bool visibleInPreferenceCenter)
        {
            return HttpPost<Dictionary<string, object>, string>(
                string.Format("/lists/{0}/customfields.json", ListID), null,
                new Dictionary<string, object>()
                {
                    { "FieldName", fieldName },
                    { "DataType", dataType.ToString() },
                    { "Options", options },
                    { "VisibleInPreferenceCenter", visibleInPreferenceCenter }
                });
        }

        public string CreateWebhook(List<string> events, string url, string payloadFormat)
        {
            return HttpPost<Dictionary<string, object>, string>(
                string.Format("/lists/{0}/webhooks.json", ListID), null,
                new Dictionary<string, object>()
                {
                    { "Events", events },
                    { "Url", url },
                    { "PayloadFormat", payloadFormat }
                });
        }

        public IEnumerable<ListCustomField> CustomFields()
        {
            return HttpGet<ListCustomField[]>(
                string.Format("/lists/{0}/customfields.json", ListID), null);
        }

        public void DeactivateWebhook(string webhookID)
        {
            HttpPut<string, string>(
                string.Format("/lists/{0}/webhooks/{1}/deactivate.json",
                ListID, System.Net.WebUtility.HtmlEncode(webhookID)), null, null);
        }

        public void Delete()
        {
            HttpDelete(string.Format("/lists/{0}.json", ListID), null);
        }

        public void DeleteCustomField(string customFieldKey)
        {
            HttpDelete(
                string.Format("/lists/{0}/customfields/{1}.json",
                ListID, System.Net.WebUtility.HtmlEncode(customFieldKey)), null);
        }

        public PagedCollection<SubscriberDetail> Deleted()
        {
            return GenericPagedSubscriberGet("deleted", "", 1, 1000, "email", "asc");
        }

        public PagedCollection<SubscriberDetail> Deleted(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("deleted", fromDate, page, pageSize, orderField, orderDirection);
        }

        public void DeleteWebhook(string webhookID)
        {
            HttpDelete(
                string.Format("/lists/{0}/webhooks/{1}.json",
                ListID, System.Net.WebUtility.HtmlEncode(webhookID)), null);
        }

        public ListDetail Details()
        {
            return HttpGet<ListDetail>(
                string.Format("/lists/{0}.json", ListID), null);
        }

        public IEnumerable<BasicSegment> Segments()
        {
            return HttpGet<BasicSegment[]>(
                string.Format("/lists/{0}/segments.json", ListID), null);
        }

        public ListStats Stats()
        {
            return HttpGet<ListStats>(
                string.Format("/lists/{0}/stats.json", ListID), null);
        }

        public bool TestWebhook(string webhookID)
        {
            HttpGet<string, ErrorResult<WebhookTestErrorResult>>(
                string.Format("/lists/{0}/webhooks/{1}/test.json",
                ListID, System.Net.WebUtility.HtmlEncode(webhookID)), null);

            return true; //an exception will be thrown if there is a problem
        }

        public PagedCollection<SubscriberDetail> Unconfirmed()
        {
            return GenericPagedSubscriberGet("unconfirmed", "", 1, 1000, "email", "asc");
        }

        public PagedCollection<SubscriberDetail> Unconfirmed(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("unconfirmed", fromDate, page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<SubscriberDetail> Unsubscribed()
        {
            return GenericPagedSubscriberGet("unsubscribed", "", 1, 1000, "email", "asc");
        }

        public PagedCollection<SubscriberDetail> Unsubscribed(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("unsubscribed", fromDate, page, pageSize, orderField, orderDirection);
        }

        public void Update(
                                                                                                                                                                                                    string title,
            string unsubscribePage,
            bool confirmedOptIn,
            string confirmationSuccessPage,
            UnsubscribeSetting unsubscribeSetting,
            bool addUnsubscribesToSuppList,
            bool scrubActiveWithSuppList)
        {
            HttpPut<ListDetailForUpdate, string>(
                string.Format("/lists/{0}.json", ListID), null,
                new ListDetailForUpdate()
                {
                    Title = title,
                    UnsubscribePage = unsubscribePage,
                    ConfirmedOptIn = confirmedOptIn,
                    ConfirmationSuccessPage = confirmationSuccessPage,
                    UnsubscribeSetting = unsubscribeSetting.ToString(),
                    AddUnsubscribesToSuppList = addUnsubscribesToSuppList,
                    ScrubActiveWithSuppList = scrubActiveWithSuppList
                });
        }

        public string UpdateCustomField(
            string customFieldKey,
            string fieldName,
            bool visibleInPreferenceCenter)
        {
            return HttpPut<Dictionary<string, object>, string>(
                string.Format("/lists/{0}/customfields/{1}.json",
                ListID, System.Net.WebUtility.HtmlEncode(customFieldKey)), null,
                new Dictionary<string, object>()
                {
                    { "FieldName", fieldName },
                    { "VisibleInPreferenceCenter", visibleInPreferenceCenter }
                });
        }

        public void UpdateCustomFieldOptions(
            string customFieldKey,
            List<string> options,
            bool keepExistingOptions)
        {
            HttpPut<object, string>(
                string.Format("/lists/{0}/customfields/{1}/options.json",
                ListID, System.Net.WebUtility.HtmlEncode(customFieldKey)), null,
                new
                {
                    KeepExistingOptions = keepExistingOptions,
                    Options = options
                });
        }

        [Obsolete("Use UpdateCustomFieldOptions instead. UpdateCustomFields will eventually be removed.", false)]
        public void UpdateCustomFields(string customFieldKey, List<string> options, bool keepExistingOptions)
        {
            UpdateCustomFieldOptions(customFieldKey, options, keepExistingOptions);
        }

        public IEnumerable<BasicWebhook> Webhooks()
        {
            return HttpGet<BasicWebhook[]>(
                string.Format("/lists/{0}/webhooks.json", ListID), null);
        }

        private PagedCollection<SubscriberDetail> GenericPagedSubscriberGet(
                    string type,
            DateTime fromDate,
            int page,
            int pageSize,
            string orderField,
            string orderDirection)
        {
            return GenericPagedSubscriberGet(type,
                fromDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), page, pageSize,
                orderField, orderDirection);
        }

        private PagedCollection<SubscriberDetail> GenericPagedSubscriberGet(string type, string fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate);
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<SubscriberDetail>>(
                string.Format("/lists/{0}/{1}.json", ListID, type), queryArguments);
        }
    }
}