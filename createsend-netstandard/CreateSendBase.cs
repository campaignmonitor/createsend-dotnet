using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace createsend_dotnet
{
    public abstract class CreateSendBase
    {
        private readonly ICreateSendOptions options;

        public CreateSendBase(AuthenticationDetails auth, ICreateSendOptions options = null)
        {
            this.options = options ?? new CreateSendOptionsWrapper();

            Authenticate(auth);
        }

        public AuthenticationDetails AuthDetails { get; set; }

        public void Authenticate(AuthenticationDetails auth)
        {
            AuthDetails = auth;
        }

        public string HttpDelete(string path, NameValueCollection queryArguments)
        {
            return HttpHelper.Delete(AuthDetails, path, queryArguments, options.BaseUri, HttpHelper.APPLICATION_JSON_CONTENT_TYPE);
        }

        public U HttpGet<U>(string path, NameValueCollection queryArguments)
        {
            return HttpGet<U, ErrorResult>(path, queryArguments);
        }

        public U HttpGet<U, EX>(string path, NameValueCollection queryArguments)
            where EX : ErrorResult
        {
            return HttpHelper.Get<U, EX>(AuthDetails, options.BaseUri, path, queryArguments);
        }

        public U HttpPost<T, U>(string path, NameValueCollection queryArguments, T payload)
            where T : class
        {
            return HttpPost<T, U, ErrorResult>(path, queryArguments, payload);
        }

        public U HttpPost<T, U, EX>(string path, NameValueCollection queryArguments, T payload)
            where T : class
            where EX : ErrorResult
        {
            return HttpHelper.Post<T, U, EX>(AuthDetails, path, queryArguments, payload, options.BaseUri, HttpHelper.APPLICATION_JSON_CONTENT_TYPE);
        }

        public U HttpPut<T, U>(string path, NameValueCollection queryArguments, T payload) where T : class
        {
            return HttpHelper.Put<T, U>(AuthDetails, path, queryArguments, payload, options.BaseUri, HttpHelper.APPLICATION_JSON_CONTENT_TYPE);
        }

        public OAuthTokenDetails RefreshToken()
        {
            if (AuthDetails == null ||
                !(AuthDetails is OAuthAuthenticationDetails) ||
                string.IsNullOrEmpty(
                    (AuthDetails as OAuthAuthenticationDetails)
                    .RefreshToken))
                throw new InvalidOperationException(
                    "You cannot refresh an OAuth token when you don't have a refresh token.");

            string refreshToken = (this.AuthDetails as OAuthAuthenticationDetails)
                .RefreshToken;
            string body = QueryHelpers.AddQueryString("", "grant_type", "refresh_token");
            body = QueryHelpers.AddQueryString(body, "refresh_token", refreshToken);

            OAuthTokenDetails newTokenDetails =
                HttpHelper.Post<string, OAuthTokenDetails, OAuthErrorResult>(
                    null, "/token", new NameValueCollection(), body,
                    options.BaseOAuthUri,
                    HttpHelper.APPLICATION_FORM_URLENCODED_CONTENT_TYPE);
            Authenticate(
                new OAuthAuthenticationDetails(
                    newTokenDetails.access_token, newTokenDetails.refresh_token));
            return newTokenDetails;
        }
    }
}