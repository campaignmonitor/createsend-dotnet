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
            return HttpHelper.Post<ClientDetail, string>("/clients.json", null,
                new ClientDetail()
                {
                    CompanyName = companyName,
                    ContactName = contactName,
                    EmailAddress = emailAddress,
                    Country = country,
                    TimeZone = timezone
                });
        }

        public ClientWithSettings Details()
        {
            return HttpHelper.Get<ClientWithSettings>(string.Format("/clients/{0}.json", ClientID), null);
        }

        public IEnumerable<CampaignDetail> Campaigns()
        {
            return HttpHelper.Get<CampaignDetail[]>(string.Format("/clients/{0}/campaigns.json", ClientID), null);
        }

        public IEnumerable<ScheduledCampaignDetail> Scheduled()
        {
            return HttpHelper.Get<ScheduledCampaignDetail[]>(string.Format("/clients/{0}/scheduled.json", ClientID), null);
        }

        public IEnumerable<DraftDetail> Drafts()
        {
            return HttpHelper.Get<DraftDetail[]>(string.Format("/clients/{0}/drafts.json", ClientID), null);
        }

        public IEnumerable<BasicList> Lists()
        {
            return HttpHelper.Get<BasicList[]>(string.Format("/clients/{0}/lists.json", ClientID), null);
        }

        public IEnumerable<BasicSegment> Segments()
        {
            return HttpHelper.Get<BasicSegment[]>(string.Format("/clients/{0}/segments.json", ClientID), null);
        }

        public PagedCollection<SuppressedSubscriber> SuppressionList(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<SuppressedSubscriber>>(string.Format("/clients/{0}/suppressionlist.json", ClientID), queryArguments);
        }

        public IEnumerable<BasicTemplate> Templates()
        {
            return HttpHelper.Get<BasicTemplate[]>(string.Format("/clients/{0}/templates.json", ClientID), null);
        }

        public void SetBasics(string companyName, string contactName, string emailAddress, string country, string timezone)
        {
            HttpHelper.Put<ClientDetail, string>(
                string.Format("/clients/{0}/setbasics.json", ClientID), null,
                new ClientDetail()
                {
                    CompanyName = companyName,
                    ContactName = contactName,
                    EmailAddress = emailAddress,
                    Country = country,
                    TimeZone = timezone
                });
        }

        public void SetAccess(string userName, string password, int accessLevel)
        {
            HttpHelper.Put<ClientAccessSettings, string>(
                string.Format("/clients/{0}/setaccess.json", ClientID), null,
                new ClientAccessSettings()
                {
                    Username = userName,
                    Password = password,
                    AccessLevel = accessLevel
                });
        }

        public void SetPAYGBilling(string currency, bool clientPays, bool canPurchaseCredits, int markupPercentage, decimal markupOnDelivery, decimal markupPerRecipient, decimal markupOnDesignSpamTest)
        {
            HttpHelper.Put<BillingOptions, string>(
                string.Format("/clients/{0}/setpaygbilling.json", ClientID), null,
                new BillingOptions()
                {
                    Currency = currency,
                    ClientPays = clientPays,
                    CanPurchaseCredits = canPurchaseCredits,
                    MarkupPercentage = markupPercentage,
                    MarkupOnDelivery = markupOnDelivery,
                    MarkupPerRecipient = markupPerRecipient,
                    MarkupOnDesignSpamTest = markupOnDesignSpamTest
                });
        }

        public void SetMonthlyBilling(string currency, bool clientPays, bool canPurchaseCredits, int markupPercentage)
        {
            HttpHelper.Put<BillingOptions, string>(
                string.Format("/clients/{0}/setmonthlybilling.json", ClientID), null,
                new BillingOptions()
                {
                    Currency = currency,
                    ClientPays = clientPays,
                    CanPurchaseCredits = canPurchaseCredits,
                    MarkupPercentage = markupPercentage
                });
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/clients/{0}.json", ClientID), null);
        }
    }
}
