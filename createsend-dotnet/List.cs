﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class List
    {
        public string ApiKey { get; set; }

        private CreateSendCredentials AuthCredentials
        {
            get { return new CreateSendCredentials(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        public string ListID { get; set; }

        public List(string listID)
        {
            ListID = listID;
        }

        public static string Create(
            string apiKey,
            string clientID,
            string title,
            string unsubscribePage,
            bool confirmedOptIn,
            string confirmationSuccessPage,
            UnsubscribeSetting unsubscribeSetting)
        {
            return HttpHelper.Post<ListDetail, string>(
                new CreateSendCredentials(apiKey, "x"), 
                string.Format("/lists/{0}.json", clientID), null,
                new ListDetail()
                {
                    Title = title,
                    UnsubscribePage = unsubscribePage,
                    ConfirmedOptIn = confirmedOptIn,
                    ConfirmationSuccessPage = confirmationSuccessPage,
                    UnsubscribeSetting = unsubscribeSetting.ToString()
                });
        }

        public static string Create(
            string clientID,
            string title,
            string unsubscribePage,
            bool confirmedOptIn,
            string confirmationSuccessPage,
            UnsubscribeSetting unsubscribeSetting)
        {
            return Create(CreateSendOptions.ApiKey, clientID, title,
                unsubscribePage, confirmedOptIn, confirmationSuccessPage,
                unsubscribeSetting);
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
            HttpHelper.Put<ListDetailForUpdate, string>(
                AuthCredentials, 
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

        public ListDetail Details()
        {
            return HttpHelper.Get<ListDetail>(AuthCredentials, string.Format("/lists/{0}.json", ListID), null);
        }

        public void Delete()
        {
            HttpHelper.Delete(AuthCredentials, string.Format("/lists/{0}.json", ListID), null);
        }

        public string CreateCustomField(string fieldName, CustomFieldDataType dataType, List<string> options)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                AuthCredentials, 
                string.Format("/lists/{0}/customfields.json", ListID), null,
                new Dictionary<string, object>() 
                { 
                    { "FieldName", fieldName }, 
                    { "DataType", dataType.ToString() }, 
                    { "Options", options } 
                });
        }

        public void DeleteCustomField(string customFieldKey)
        {
            HttpHelper.Delete(AuthCredentials, string.Format("/lists/{0}/customfields/{1}.json", ListID, System.Web.HttpUtility.UrlEncode(customFieldKey)), null);
        }

        public IEnumerable<ListCustomField> CustomFields()
        {
            return HttpHelper.Get<ListCustomField[]>(AuthCredentials, string.Format("/lists/{0}/customfields.json", ListID), null);
        }

        public void UpdateCustomFields(string customFieldKey, List<string> options, bool keepExistingOptions)
        {
            HttpHelper.Put<object, string>(
                AuthCredentials, 
                string.Format("/lists/{0}/customfields/{1}/options.json", ListID, System.Web.HttpUtility.UrlEncode(customFieldKey)), null,
                new { KeepExistingOptions = keepExistingOptions, Options = options });
        }

        public IEnumerable<BasicSegment> Segments()
        {
            return HttpHelper.Get<BasicSegment[]>(AuthCredentials, string.Format("/lists/{0}/segments.json", ListID), null);
        }

        public ListStats Stats()
        {
            return HttpHelper.Get<ListStats>(AuthCredentials, string.Format("/lists/{0}/stats.json", ListID), null);
        }

        public PagedCollection<SubscriberDetail> Active(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("active", fromDate, page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<SubscriberDetail> Unsubscribed(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("unsubscribed", fromDate, page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<SubscriberDetail> Bounced(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("bounced", fromDate, page, pageSize, orderField, orderDirection);
        }

        public PagedCollection<SubscriberDetail> Deleted(DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            return GenericPagedSubscriberGet("deleted", fromDate, page, pageSize, orderField, orderDirection);
        }

        private PagedCollection<SubscriberDetail> GenericPagedSubscriberGet(string type, DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<SubscriberDetail>>(AuthCredentials, string.Format("/lists/{0}/{1}.json", ListID, type), queryArguments);
        }

        public IEnumerable<BasicWebhook> Webhooks()
        {
            return HttpHelper.Get<BasicWebhook[]>(AuthCredentials, string.Format("/lists/{0}/webhooks.json", ListID), null);
        }

        public string CreateWebhook(List<string> events, string url, string payloadFormat)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                AuthCredentials, 
                string.Format("/lists/{0}/webhooks.json", ListID), null,
                new Dictionary<string, object>() 
                { 
                    { "Events", events }, 
                    { "Url", url }, 
                    { "PayloadFormat", payloadFormat } 
                });
        }

        public bool TestWebhook(string webhookID)
        {
            HttpHelper.Get<string, ErrorResult<WebhookTestErrorResult>>(
                AuthCredentials, 
                string.Format("/lists/{0}/webhooks/{1}/test.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null);
          
            return true; //an exception will be thrown if there is a problem
        }

        public void DeleteWebhook(string webhookID)
        {
            HttpHelper.Delete(AuthCredentials, string.Format("/lists/{0}/webhooks/{1}.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null);
        }

        public void ActivateWebhook(string webhookID)
        {
            HttpHelper.Put<string, string>(AuthCredentials, string.Format("/lists/{0}/webhooks/{1}/activate.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null, null);
        }

        public void DeactivateWebhook(string webhookID)
        {
            HttpHelper.Put<string, string>(AuthCredentials, string.Format("/lists/{0}/webhooks/{1}/deactivate.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null, null);
        }
    }
}
