using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public abstract class AuthenticationDetails
    {
    }

    public class OAuthAuthenticationDetails : AuthenticationDetails
    {
        public OAuthAuthenticationDetails(
            string accessToken,
            string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class ApiKeyAuthenticationDetails : AuthenticationDetails
    {
        public string ApiKey { get; set; }

        public ApiKeyAuthenticationDetails(string apiKey)
        {
            ApiKey = apiKey;
        }
    }

    internal sealed class ClientApiKey : ApiKeyAuthenticationDetails
    {
        public ClientApiKey(string apiKey) 
            : base(apiKey)
        {
        }
    }

    internal sealed class AccountApiKey : ApiKeyAuthenticationDetails, IProvideClientId
    {
        public string ClientId { get; private set; }
        public AccountApiKey(string apiKey, string clientId = null) : base(apiKey)
        {
            ClientId = clientId;
        }
    }

    internal sealed class OAuthWithClientId : OAuthAuthenticationDetails, IProvideClientId
    {
        public string ClientId { get; private set; }
        public OAuthWithClientId(
            string accessToken,
            string refreshToken, 
            string clientId) : base(accessToken, refreshToken)
        {
            if(clientId == null) throw new ArgumentNullException("clientId");

            ClientId = clientId;
        }
    }

    public interface IProvideClientId
    {
        string ClientId { get; }
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