using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace createsend_dotnet
{
    public class General : CreateSendBase
    {
        public General() : base(null) { }
        public General(AuthenticationDetails auth) : base(auth) { }

        public static string AuthorizeUrl(
            int clientID,
            string clientSecret,
            string redirectUri,
            string scope)
        {
            return AuthorizeUrl(
                clientID,
                clientSecret,
                redirectUri,
                scope,
                null);
        }

        public static string AuthorizeUrl(
            int clientID,
            string clientSecret,
            string redirectUri,
            string scope,
            string state)
        {
            string result = CreateSendOptions.BaseOAuthUri;
            result += string.Format(
                "?client_id={0}&client_secret={1}&redirect_uri={2}&scope={3}",
                clientID.ToString(), HttpUtility.UrlEncode(clientSecret),
                HttpUtility.UrlEncode(redirectUri),
                HttpUtility.UrlEncode(scope));
            if (!string.IsNullOrEmpty(state))
                result += "&state=" + HttpUtility.UrlEncode(state);
            return result;
        }

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
