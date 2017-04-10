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
            string redirectUri,
            string scope)
        {
            return AuthorizeUrl(
                clientID,
                redirectUri,
                scope,
                null);
        }

        public static string AuthorizeUrl(
            int clientID,
            string redirectUri,
            string scope,
            string state)
        {
            string result = CreateSendOptions.BaseOAuthUri;
            result += string.Format(
                "?client_id={0}&redirect_uri={1}&scope={2}",
                clientID.ToString(), HttpUtility.UrlEncode(redirectUri),
                HttpUtility.UrlEncode(scope));
            if (!string.IsNullOrEmpty(state))
                result += "&state=" + HttpUtility.UrlEncode(state);
            return result;
        }

        public static OAuthTokenDetails ExchangeToken(
            int clientID,
            string clientSecret,
            string redirectUri,
            string code)
        {
            string body = "grant_type=authorization_code";
            body += string.Format("&client_id={0}", clientID.ToString());
            body += string.Format("&client_secret={0}", HttpUtility.UrlEncode(clientSecret));
            body += string.Format("&redirect_uri={0}", HttpUtility.UrlEncode(redirectUri));
            body += string.Format("&code={0}", HttpUtility.UrlEncode(code));

            return HttpHelper.Post<string, OAuthTokenDetails, OAuthErrorResult>(
                null, "/token", new NameValueCollection(), body,
                CreateSendOptions.BaseOAuthUri,
                HttpHelper.APPLICATION_FORM_URLENCODED_CONTENT_TYPE);
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

        public string ExternalSessionUrl(
            ExternalSessionOptions options)
        {
            return HttpPut<ExternalSessionOptions, ExternalSessionResult>(
                "/externalsession.json", null, options).SessionUrl;
        }
    }
}
