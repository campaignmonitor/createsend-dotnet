using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace createsend_dotnet
{
    public class BasicClient
    {
        public string ClientID { get; set; }
        public string Name { get; set; }
    }

    public class Clients : List<BasicClient> { }

    public class ClientWithSettings
    {
        public string ApiKey { get; set; }
        public ClientDetail BasicDetails { get; set; }
        public ClientAccessSettings AccessDetails { get; set; }
        public BillingDetail BillingDetails { get; set; }
    }
    
    public class ClientDetail
    {
        public string ClientID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string TimeZone { get; set; }        
    }      

    public class BillingOptions
    {
        public string Currency { get; set; }
        public bool ClientPays { get; set; }
        public int MarkupPercentage { get; set; }
        public bool CanPurchaseCredits { get; set; }
        public decimal? MarkupOnDelivery { get; set; }
        public decimal? MarkupPerRecipient { get; set; }
        public decimal? MarkupOnDesignSpamTest { get; set; }        
    }

    public class BillingDetail : BillingOptions
    {
        public string CurrentTier { get; set; }
        public decimal CurrentMonthlyRate { get; set; }
        public decimal BaseDeliveryRate { get; set; }
        public decimal BaseRatePerRecipient { get; set; }
        public decimal BaseDesignSpamTestRate { get; set; }
    }

    public class ClientAccessSettings
    {
        public int AccessLevel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
