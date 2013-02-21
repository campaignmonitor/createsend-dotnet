using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class General : CreateSendBase
    {
        public General() : base(null) { }
        public General(AuthenticationDetails auth) : base(auth) { }

        public string ApiKey(string siteUrl, string username, string password)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("SiteUrl", siteUrl);

            ApiKeyResult result = HttpHelper.Get<ApiKeyResult>(
                new BasicAuthAuthenticationDetails(username, password),
                "/apikey.json", queryArguments);
            return result.ApiKey;
        }

        public DateTime SystemDate()
        {
            return HttpGet<SystemDateResult>("/systemdate.json", null).SystemDate;
        }

        public IEnumerable<string> Countries(string apiKey)
        {
            return HttpGet<string[]>("/countries.json", null);
        }

        public IEnumerable<string> Timezones()
        {
            return HttpGet<string[]>("/timezones.json", null);
        }

        public IEnumerable<BasicClient> Clients()
        {
            return HttpGet<Clients>("/clients.json", null);
        }

        public BillingDetails BillingDetails()
        {
            return HttpGet<BillingDetails>("/billingdetails.json", null);
        }
    }
}
