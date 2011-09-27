using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Client
    {
        public string ClientID { get; set; }

        public Client(string clientID)
        {
            ClientID = clientID;
        }

        public static string Create(string companyName, string contactName, string emailAddress, string country, string timezone)
        {
            string json = HttpHelper.Post("/clients.json", null, JavaScriptConvert.SerializeObject(
                new ClientDetail() { CompanyName = companyName, ContactName = contactName, EmailAddress = emailAddress, Country = country, TimeZone = timezone })
                );
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public ClientWithSettings Details()
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}.json", ClientID), null);
            return JavaScriptConvert.DeserializeObject<ClientWithSettings>(json);
        }

        public IEnumerable<CampaignDetail> Campaigns()
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/campaigns.json", ClientID), null);
            return JavaScriptConvert.DeserializeObject<CampaignDetail[]>(json);
        }

        public IEnumerable<ScheduledCampaignDetail> Scheduled()
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/scheduled.json", ClientID), null);
            return JavaScriptConvert.DeserializeObject<ScheduledCampaignDetail[]>(json);
        }

        public IEnumerable<DraftDetail> Drafts()
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/drafts.json", ClientID), null);
            return JavaScriptConvert.DeserializeObject<DraftDetail[]>(json);
        }

        public IEnumerable<BasicList> Lists()
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/lists.json", ClientID), null);
            return JavaScriptConvert.DeserializeObject<BasicList[]>(json);
        }

        public IEnumerable<BasicSegment> Segments()
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/segments.json", ClientID), null);
            return JavaScriptConvert.DeserializeObject<BasicSegment[]>(json);
        }

        public PagedCollection<SuppressedSubscriber> SuppressionList(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            string json = HttpHelper.Get(string.Format("/clients/{0}/suppressionlist.json", ClientID), queryArguments);
            return JavaScriptConvert.DeserializeObject<PagedCollection<SuppressedSubscriber>>(json);
        }

        public IEnumerable<BasicTemplate> Templates()
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/templates.json", ClientID), null);
            return JavaScriptConvert.DeserializeObject<BasicTemplate[]>(json);
        }

        public void SetBasics(string companyName, string contactName, string emailAddress, string country, string timezone)
        {
            HttpHelper.Put(string.Format("/clients/{0}/setbasics.json", ClientID), null, JavaScriptConvert.SerializeObject(
                new ClientDetail() { CompanyName = companyName, ContactName = contactName, EmailAddress = emailAddress, Country = country, TimeZone = timezone })
            );
        }

        public void SetAccess(string userName, string password, int accessLevel)
        {
            HttpHelper.Put(string.Format("/clients/{0}/setaccess.json", ClientID), null, JavaScriptConvert.SerializeObject(
                new ClientAccessSettings() { Username = userName, Password = password, AccessLevel = accessLevel })
            );
        }

        public void SetPAYGBilling(string currency, bool clientPays, bool canPurchaseCredits, int markupPercentage, decimal markupOnDelivery, decimal markupPerRecipient, decimal markupOnDesignSpamTest)
        {
            HttpHelper.Put(string.Format("/clients/{0}/setpaygbilling.json", ClientID), null, JavaScriptConvert.SerializeObject(
                new BillingOptions() { Currency = currency, ClientPays = clientPays, CanPurchaseCredits = canPurchaseCredits, MarkupPercentage = markupPercentage, MarkupOnDelivery = markupOnDelivery, MarkupPerRecipient = markupPerRecipient, MarkupOnDesignSpamTest = markupOnDesignSpamTest })
            );
        }

        public void SetMonthlyBilling(string currency, bool clientPays, bool canPurchaseCredits, int markupPercentage)
        {
            HttpHelper.Put(string.Format("/clients/{0}/setmonthlybilling.json", ClientID), null, JavaScriptConvert.SerializeObject(
                new BillingOptions() { Currency = currency, ClientPays = clientPays, CanPurchaseCredits = canPurchaseCredits, MarkupPercentage = markupPercentage })
            );
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/clients/{0}.json", ClientID), null);
        }
    }
}
