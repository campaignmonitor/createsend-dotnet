using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public abstract class AuthenticationDetails { }

    public class OAuthAuthenticationDetails : AuthenticationDetails
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public OAuthAuthenticationDetails(
            string accessToken,
            string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }

    public class ApiKeyAuthenticationDetails : AuthenticationDetails
    {
        public string ApiKey { get; set; }

        public ApiKeyAuthenticationDetails(string apiKey)
        {
            ApiKey = apiKey;
        }
    }

    public class BasicAuthAuthenticationDetails : AuthenticationDetails
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public BasicAuthAuthenticationDetails(
            string username,
            string password)
        {
            Username = username;
            Password = password;
        }
    }
}