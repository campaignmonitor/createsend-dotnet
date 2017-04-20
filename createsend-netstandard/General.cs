using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.AspNetCore.WebUtilities;

namespace createsend_dotnet
{
    public class General : CreateSendBase
    {
        public General() : base(null)
        {
        }

        public General(AuthenticationDetails auth) : base(auth)
        {
        }

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
            result += QueryHelpers.AddQueryString(result, "client_id", clientID.ToString());
            result += QueryHelpers.AddQueryString(result, "redirect_uri", redirectUri);
            if (!string.IsNullOrEmpty(state))
                result += QueryHelpers.AddQueryString(result, "state", state);
            result += QueryHelpers.AddQueryString(result, "scope", scope);

            return result;
        }

        public static OAuthTokenDetails ExchangeToken(
            int clientID,
            string clientSecret,
            string redirectUri,
            string code)
        {
            string body = "grant_type=authorization_code";
            body += QueryHelpers.AddQueryString(body, "client_id", clientID.ToString());
            body += QueryHelpers.AddQueryString(body, "client_secret", clientSecret);
            body += QueryHelpers.AddQueryString(body, "redirect_uri", redirectUri);
            body += QueryHelpers.AddQueryString(body, "code", code);

            return HttpHelper.Post<string, OAuthTokenDetails, OAuthErrorResult>(
                null, "/token", new NameValueCollection(), body,
                CreateSendOptions.BaseOAuthUri,
                HttpHelper.APPLICATION_FORM_URLENCODED_CONTENT_TYPE);
        }

        public BillingDetails BillingDetails()
        {
            return HttpGet<BillingDetails>("/billingdetails.json", null);
        }

        public IEnumerable<BasicClient> Clients()
        {
            return HttpGet<Clients>("/clients.json", null);
        }

        public IEnumerable<string> Countries(string apiKey)
        {
            return HttpGet<string[]>("/countries.json", null);
        }

        public string ExternalSessionUrl(
            ExternalSessionOptions options)
        {
            return HttpPut<ExternalSessionOptions, ExternalSessionResult>(
                "/externalsession.json", null, options).SessionUrl;
        }

        public DateTime SystemDate()
        {
            return HttpGet<SystemDateResult>("/systemdate.json", null).SystemDate;
        }

        public IEnumerable<string> Timezones()
        {
            return HttpGet<string[]>("/timezones.json", null);
        }
    }
}