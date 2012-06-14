using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace createsend_dotnet
{
    public class Client
    {
        public string ApiKey { get; set; }

        private NetworkCredential AuthCredentials
        {
            get { return new NetworkCredential(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        public string ClientID { get; set; }

        public Client(string clientID)
        {
            ClientID = clientID;
        }

		public static string Create(string apiKey, string companyName, string country, string timezone)
		{
			return HttpHelper.Post<ClientDetail, string>(
				new NetworkCredential(apiKey, "x"),
				"/clients.json",
				null,
				new ClientDetail()
				{
					CompanyName = companyName,
					Country = country,
					TimeZone = timezone
				});
		}

		[Obsolete("Use the other version of Create - without contact name and email address - and instead add People to this Client using Person.Add.")]
        public static string Create(string apiKey, string companyName, string contactName, string emailAddress, string country, string timezone)
        {
            return HttpHelper.Post<ClientDetail, string>(
                new NetworkCredential(apiKey, "x"), 
                "/clients.json", 
                null,
                new ClientDetail()
                {
                    CompanyName = companyName,
                    ContactName = contactName,
                    EmailAddress = emailAddress,
                    Country = country,
                    TimeZone = timezone
                });
        }

        public static string Create(string companyName, string contactName, string emailAddress, string country, string timezone)
        {
            return Create(CreateSendOptions.ApiKey, companyName, contactName, emailAddress, country, timezone);
        }

        public ClientWithSettings Details()
        {
            return HttpHelper.Get<ClientWithSettings>(AuthCredentials, string.Format("/clients/{0}.json", ClientID), null);
        }

        public IEnumerable<CampaignDetail> Campaigns()
        {
            return HttpHelper.Get<CampaignDetail[]>(AuthCredentials, string.Format("/clients/{0}/campaigns.json", ClientID), null);
        }

        public IEnumerable<ScheduledCampaignDetail> Scheduled()
        {
            return HttpHelper.Get<ScheduledCampaignDetail[]>(AuthCredentials, string.Format("/clients/{0}/scheduled.json", ClientID), null);
        }

        public IEnumerable<DraftDetail> Drafts()
        {
            return HttpHelper.Get<DraftDetail[]>(AuthCredentials, string.Format("/clients/{0}/drafts.json", ClientID), null);
        }

        public IEnumerable<BasicList> Lists()
        {
            return HttpHelper.Get<BasicList[]>(AuthCredentials, string.Format("/clients/{0}/lists.json", ClientID), null);
        }

        public IEnumerable<BasicSegment> Segments()
        {
            return HttpHelper.Get<BasicSegment[]>(AuthCredentials, string.Format("/clients/{0}/segments.json", ClientID), null);
        }

        public PagedCollection<SuppressedSubscriber> SuppressionList(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpHelper.Get<PagedCollection<SuppressedSubscriber>>(AuthCredentials, string.Format("/clients/{0}/suppressionlist.json", ClientID), queryArguments);
        }

        public IEnumerable<BasicTemplate> Templates()
        {
            return HttpHelper.Get<BasicTemplate[]>(AuthCredentials, string.Format("/clients/{0}/templates.json", ClientID), null);
        }

		public void SetBasics(string companyName, string country, string timezone)
		{
			HttpHelper.Put<ClientDetail, string>(
				AuthCredentials,
				string.Format("/clients/{0}/setbasics.json", ClientID), null,
				new ClientDetail()
				{
					CompanyName = companyName,
					Country = country,
					TimeZone = timezone
				});
		}

		[Obsolete("Use the other version of SetBasics - without contact name and email address - and instead add or edit People in this Client using Person.Add")]
        public void SetBasics(string companyName, string contactName, string emailAddress, string country, string timezone)
        {
            HttpHelper.Put<ClientDetail, string>(
                AuthCredentials, 
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

		[Obsolete("Access should now be set on individual People in your client (using Person.Add or Person.Update) rather than on the Client itself.")]
        public void SetAccess(string userName, string password, int accessLevel)
        {
            HttpHelper.Put<ClientAccessSettings, string>(
                AuthCredentials, 
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
                AuthCredentials, 
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
                AuthCredentials, 
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
            HttpHelper.Delete(AuthCredentials, string.Format("/clients/{0}.json", ClientID), null);
        }

        public string GetPrimaryContact()
        {
            return HttpHelper.Get<PersonResult>(AuthCredentials, string.Format("/clients/{0}/primarycontact.json", ClientID), null).EmailAddress;
        }

        public string SetPrimaryContact(string emailAddress)
        {
            return HttpHelper.Put<string, PersonResult>(AuthCredentials, string.Format("/clients/{0}/primarycontact.json", ClientID), new NameValueCollection{{"email", emailAddress}}, null).EmailAddress;
        }

        public IEnumerable<PersonDetails> People()
        {
            return HttpHelper.Get<IEnumerable<PersonDetails>>(AuthCredentials, string.Format("/clients/{0}/people.json", ClientID), null);
        }

    }
}
