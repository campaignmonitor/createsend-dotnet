using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class Client : CreateSendBase
    {
        public string ClientID { get; set; }

        public Client(AuthenticationDetails auth, string clientID)
            : base(auth)
        {
            ClientID = clientID;
        }

        public static string Create(AuthenticationDetails auth, string companyName, string country, string timezone)
        {
            return HttpHelper.Post<ClientDetail, string>(
                auth, "/clients.json", null,
                new ClientDetail()
                {
                    CompanyName = companyName,
                    Country = country,
                    TimeZone = timezone
                });
        }

        public ClientWithSettings Details()
        {
            return HttpGet<ClientWithSettings>(string.Format("/clients/{0}.json", ClientID), null);
        }

        public IEnumerable<CampaignDetail> Campaigns()
        {
            return HttpGet<CampaignDetail[]>(string.Format("/clients/{0}/campaigns.json", ClientID), null);
        }

        public IEnumerable<ScheduledCampaignDetail> Scheduled()
        {
            return HttpGet<ScheduledCampaignDetail[]>(string.Format("/clients/{0}/scheduled.json", ClientID), null);
        }

        public IEnumerable<DraftDetail> Drafts()
        {
            return HttpGet<DraftDetail[]>(string.Format("/clients/{0}/drafts.json", ClientID), null);
        }

        public IEnumerable<BasicList> Lists()
        {
            return HttpGet<BasicList[]>(string.Format("/clients/{0}/lists.json", ClientID), null);
        }

        public IEnumerable<ListForEmail> ListsForEmail(string email)
        {
            NameValueCollection args = new NameValueCollection();
            args.Add("email", email);
            return HttpGet<ListForEmail[]>(string.Format("/clients/{0}/listsforemail.json", ClientID), args);
        }

        public IEnumerable<BasicSegment> Segments()
        {
            return HttpGet<BasicSegment[]>(string.Format("/clients/{0}/segments.json", ClientID), null);
        }

        public PagedCollection<SuppressedSubscriber> SuppressionList(int page, int pageSize, string orderField, string orderDirection)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("page", page.ToString());
            queryArguments.Add("pagesize", pageSize.ToString());
            queryArguments.Add("orderfield", orderField);
            queryArguments.Add("orderdirection", orderDirection);

            return HttpGet<PagedCollection<SuppressedSubscriber>>(string.Format("/clients/{0}/suppressionlist.json", ClientID), queryArguments);
        }

        public void Suppress(string[] emails)
        {
            HttpPost<SuppressionDetails, string>(
                string.Format("/clients/{0}/suppress.json", ClientID), null,
                new SuppressionDetails { EmailAddresses = emails });
        }

        public void Unsuppress(string email)
        {
            HttpPut<string, PersonResult>(
                string.Format("/clients/{0}/unsuppress.json", ClientID),
                new NameValueCollection { { "email", email } }, null);
        }

        public IEnumerable<BasicTemplate> Templates()
        {
            return HttpGet<BasicTemplate[]>(string.Format("/clients/{0}/templates.json", ClientID), null);
        }

        public void SetBasics(string companyName, string country, string timezone)
        {
            HttpPut<ClientDetail, string>(
                string.Format("/clients/{0}/setbasics.json", ClientID), null,
                new ClientDetail()
                {
                    CompanyName = companyName,
                    Country = country,
                    TimeZone = timezone
                });
        }

        public void SetPAYGBilling(string currency, bool clientPays, bool canPurchaseCredits, int markupPercentage, decimal markupOnDelivery, decimal markupPerRecipient, decimal markupOnDesignSpamTest)
        {
            HttpPut<BillingOptions, string>(
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
            SetMonthlyBilling(currency, clientPays, canPurchaseCredits, markupPercentage, null);
        }

        public void SetMonthlyBilling(string currency, bool clientPays, bool canPurchaseCredits, int markupPercentage, MonthlyScheme scheme)
        {
            SetMonthlyBilling(currency, clientPays, canPurchaseCredits, markupPercentage, (MonthlyScheme?)scheme);
        }

        private void SetMonthlyBilling(string currency, bool clientPays, bool canPurchaseCredits, int markupPercentage, MonthlyScheme? scheme)
        {
            HttpPut<BillingOptions, string>(
                string.Format("/clients/{0}/setmonthlybilling.json", ClientID), null,
                new BillingOptions()
                {
                    Currency = currency,
                    ClientPays = clientPays,
                    CanPurchaseCredits = canPurchaseCredits,
                    MarkupPercentage = markupPercentage,
                    MonthlyScheme = scheme
                });
        }

        /// <summary>
        /// Transfer credits to or from this client.
        /// </summary>
        /// <param name="credits">The number of credits to transfer. This
        /// value may be either positive if you want to allocate credits
        /// from your account to the client, or negative if you want to
        /// deduct credits from the client back into your account.</param>
        /// <param name="canUseMyCreditsWhenTheyRunOut">If set to true, will
        /// allow the client to continue sending using your credits or payment
        /// details once they run out of credits, and if set to false, will
        /// prevent the client from using your credits to continue sending
        /// until you allocate more credits to them.</param>
        /// <returns>The details of the credits transfer, including the credits
        /// in your account now, as well as the credits belonging to the client
        /// now.</returns>
        public CreditsTransferResult TransferCredits(
            int credits, bool canUseMyCreditsWhenTheyRunOut)
        {
            return HttpPost<CreditsTransferDetails, CreditsTransferResult>(
                string.Format("/clients/{0}/credits.json", ClientID), null,
                new CreditsTransferDetails
                {
                    Credits = credits,
                    CanUseMyCreditsWhenTheyRunOut =
                        canUseMyCreditsWhenTheyRunOut
                });
        }

        public void Delete()
        {
            HttpDelete(string.Format("/clients/{0}.json", ClientID), null);
        }

        public string GetPrimaryContact()
        {
            return HttpGet<PersonResult>(string.Format("/clients/{0}/primarycontact.json", ClientID), null).EmailAddress;
        }

        public string SetPrimaryContact(string emailAddress)
        {
            return HttpPut<string, PersonResult>(string.Format("/clients/{0}/primarycontact.json", ClientID), new NameValueCollection { { "email", emailAddress } }, null).EmailAddress;
        }

        public IEnumerable<PersonDetails> People()
        {
            return HttpGet<IEnumerable<PersonDetails>>(string.Format("/clients/{0}/people.json", ClientID), null);
        }
    }
}
