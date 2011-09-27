﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class List
    {
        public string ListID { get; set; }

        public List(string listID)
        {
            ListID = listID;
        }

        public static string Create(string clientID, string title, string unsubscribePage, bool confirmedOptIn, string confirmationSuccessPage)
        {
            string json = HttpHelper.Post(string.Format("/lists/{0}.json", clientID), null, JsonConvert.SerializeObject(
                new ListDetail() { Title = title, UnsubscribePage = unsubscribePage, ConfirmedOptIn = confirmedOptIn, ConfirmationSuccessPage = confirmationSuccessPage })
                );
            return JsonConvert.DeserializeObject<string>(json);
        }

        public void Update(string title, string unsubscribePage, bool confirmedOptIn, string confirmationSuccessPage)
        {
            HttpHelper.Put(string.Format("/lists/{0}.json", ListID), null, JsonConvert.SerializeObject(
                new ListDetail() { Title = title, UnsubscribePage = unsubscribePage, ConfirmedOptIn = confirmedOptIn, ConfirmationSuccessPage = confirmationSuccessPage })
                );
        }

        public ListDetail Details()
        {
            string json = HttpHelper.Get(string.Format("/lists/{0}.json", ListID), null);
            return JsonConvert.DeserializeObject<ListDetail>(json);
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/lists/{0}.json", ListID), null);
        }

        public string CreateCustomField(string fieldName, CustomFieldDataType dataType, List<string> options)
        {
            string json = HttpHelper.Post(string.Format("/lists/{0}/customfields.json", ListID), null, JsonConvert.SerializeObject(
                new Dictionary<string, object>() { { "FieldName", fieldName }, { "DataType", dataType.ToString() }, { "Options", options } })
                );
            return JsonConvert.DeserializeObject<string>(json);
        }

        public void DeleteCustomField(string customFieldKey)
        {
            HttpHelper.Delete(string.Format("/lists/{0}/customfields/{1}.json", ListID, System.Web.HttpUtility.UrlEncode(customFieldKey)), null);
        }

        public IEnumerable<ListCustomField> CustomFields()
        {
            string json = HttpHelper.Get(string.Format("/lists/{0}/customfields.json", ListID), null);
            return JsonConvert.DeserializeObject<ListCustomField[]>(json);
        }

        public void UpdateCustomFields(string customFieldKey, List<string> options, bool keepExistingOptions)
        {
            HttpHelper.Put(string.Format("/lists/{0}/customfields/{1}/options.json", ListID, System.Web.HttpUtility.UrlEncode(customFieldKey)), null, 
                JsonConvert.SerializeObject(
                    new { KeepExistingOptions = keepExistingOptions, Options = options })
                );
        }

        public IEnumerable<BasicSegment> Segments()
        {
            string json = HttpHelper.Get(string.Format("/lists/{0}/segments.json", ListID), null);
            return JsonConvert.DeserializeObject<BasicSegment[]>(json);
        }

        public ListStats Stats()
        {
            string json = HttpHelper.Get(string.Format("/lists/{0}/stats.json", ListID), null);
            return JsonConvert.DeserializeObject<ListStats>(json);
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

        private PagedCollection<SubscriberDetail> GenericPagedSubscriberGet(string type, DateTime fromDate, int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("date", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/lists/{0}/{1}.json", ListID, type), queryArguments);
            return JsonConvert.DeserializeObject<PagedCollection<SubscriberDetail>>(json);
        }

        public IEnumerable<BasicWebhook> Webhooks()
        {
            string json = HttpHelper.Get(string.Format("/lists/{0}/webhooks.json", ListID), null);
            return JsonConvert.DeserializeObject<BasicWebhook[]>(json);
        }

        public string CreateWebhook(List<string> events, string url, string payloadFormat)
        {
            string json = HttpHelper.Post(string.Format("/lists/{0}/webhooks.json", ListID), null, JsonConvert.SerializeObject(
                new Dictionary<string, object>() { { "Events", events }, { "Url", url }, { "PayloadFormat", payloadFormat } })
                );
            return JsonConvert.DeserializeObject<string>(json);
        }

        public bool TestWebhook(string webhookID)
        {
            try
            {
                HttpHelper.Get(string.Format("/lists/{0}/webhooks/{1}/test.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null);
            }
            catch (CreatesendException ex)
            {
                if (!ex.Data.Contains("ErrorResult") && ex.Data.Contains("ErrorResponse"))
                {
                    ErrorResult<WebhookTestErrorResult> result = JsonConvert.DeserializeObject<ErrorResult<WebhookTestErrorResult>>(ex.Data["ErrorResponse"].ToString());
                    ex.Data.Add("ErrorResult", result);
                }

                throw ex;
            }
            catch (Exception ex) { throw ex; }

            return true; //an exception will be thrown if there is a problem
        }

        public void DeleteWebhook(string webhookID)
        {
            HttpHelper.Delete(string.Format("/lists/{0}/webhooks/{1}.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null);
        }

        public void ActivateWebhook(string webhookID)
        {
            HttpHelper.Put(string.Format("/lists/{0}/webhooks/{1}/activate.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null, "");
        }

        public void DeactivateWebhook(string webhookID)
        {
            HttpHelper.Put(string.Format("/lists/{0}/webhooks/{1}/deactivate.json", ListID, System.Web.HttpUtility.UrlEncode(webhookID)), null, "");
        }
    }
}
