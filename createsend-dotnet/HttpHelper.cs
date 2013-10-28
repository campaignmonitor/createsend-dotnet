using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using System.Reflection;

namespace createsend_dotnet
{
    public static class CreateSendOptions
    {
        static string base_uri;
        static string base_oauth_uri;

        static CreateSendOptions()
        {
            base_uri = string.IsNullOrEmpty(
                ConfigurationManager.AppSettings["base_uri"]) ?
                "https://api.createsend.com/api/v3" :
                ConfigurationManager.AppSettings["base_uri"];
            base_oauth_uri = string.IsNullOrEmpty(
                ConfigurationManager.AppSettings["base_oauth_uri"]) ?
                "https://api.createsend.com/oauth" :
                ConfigurationManager.AppSettings["base_oauth_uri"];
        }

        public static string BaseUri
        {
            get { return base_uri; }
            set { base_uri = value; }
        }

        public static string BaseOAuthUri
        {
            get { return base_oauth_uri; }
            set { base_oauth_uri = value; }
        }

        public static string VersionNumber
        {
            get
            {
                return "3.1.1";
            }
        }

    }

    public class HttpHelper
    {
        public const string APPLICATION_JSON_CONTENT_TYPE = "application/json";
        public const string APPLICATION_FORM_URLENCODED_CONTENT_TYPE = "application/x-www-form-urlencoded";

        public static U Get<U>(
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments)
        {
            return Get<U, ErrorResult>(auth, path, queryArguments);
        }

        public static U Get<U, EX>(
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments)
            where EX : ErrorResult
        {
            return MakeRequest<string, U, EX>("GET", auth, path, queryArguments, null);
        }

        public static U Post<T, U>(
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments,
            T payload)
            where T : class
        {
            return Post<T, U, ErrorResult>(auth, path, queryArguments, payload);
        }

        public static U Post<T, U, EX>(
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments,
            T payload)
            where T : class
            where EX : ErrorResult
        {
            return MakeRequest<T, U, EX>("POST", auth, path, queryArguments, payload);
        }

        public static U Post<T, U, EX>(
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments,
            T payload,
            string baseUri,
            string contentType)
            where T : class
            where EX : ErrorResult
        {
            return MakeRequest<T, U, EX>("POST", auth, path, queryArguments, payload, baseUri, contentType);
        }

        public static U Put<T, U>(AuthenticationDetails auth, string path, NameValueCollection queryArguments, T payload) where T : class
        {
            return MakeRequest<T, U, ErrorResult>("PUT", auth, path, queryArguments, payload);
        }

        public static string Delete(AuthenticationDetails auth, string path, NameValueCollection queryArguments)
        {
            return MakeRequest<string, string, ErrorResult>("DELETE", auth, path, queryArguments, null);
        }

        static U MakeRequest<T, U, EX>(
            string method,
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments,
            T payload)
            where T : class
            where EX : ErrorResult
        {
            return MakeRequest<T, U, EX>(method, auth, path, queryArguments,
                payload, CreateSendOptions.BaseUri, APPLICATION_JSON_CONTENT_TYPE);
        }

        static U MakeRequest<T, U, EX>(
            string method,
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments,
            T payload,
            string baseUri,
            string contentType)
            where T : class
            where EX : ErrorResult
        {
            JsonSerializerSettings serialiserSettings = new JsonSerializerSettings();
            serialiserSettings.NullValueHandling = NullValueHandling.Ignore;
            serialiserSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

            string uri = baseUri + path + NameValueCollectionExtension.ToQueryString(queryArguments);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = method;
            req.ContentType = contentType;
            req.AutomaticDecompression = DecompressionMethods.GZip;

            if (auth != null)
            {
                if (auth is OAuthAuthenticationDetails)
                {
                    OAuthAuthenticationDetails oauthDetails = auth as OAuthAuthenticationDetails;
                    req.Headers["Authorization"] = "Bearer " + oauthDetails.AccessToken;
                }
                else if (auth is ApiKeyAuthenticationDetails)
                {
                    ApiKeyAuthenticationDetails apiKeyDetails = auth as ApiKeyAuthenticationDetails;
                    req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(
                        Encoding.Default.GetBytes(apiKeyDetails.ApiKey + ":x"));
                }
                else if (auth is BasicAuthAuthenticationDetails)
                {
                    BasicAuthAuthenticationDetails basicDetails = auth as BasicAuthAuthenticationDetails;
                    req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(
                        Encoding.Default.GetBytes(basicDetails.Username + ":" + basicDetails.Password));
                }
            }

            req.UserAgent = string.Format("createsend-dotnet-#{0} .Net: {1} OS: {2} DLL: {3}",
                CreateSendOptions.VersionNumber, Environment.Version, Environment.OSVersion, Assembly.GetExecutingAssembly().FullName);

            if (method != "GET")
            {
                if (payload != null)
                {
                    using (System.IO.StreamWriter os = new System.IO.StreamWriter(req.GetRequestStream()))
                    {
                        if (contentType == APPLICATION_FORM_URLENCODED_CONTENT_TYPE)
                            os.Write(payload);
                        else
                            os.Write(JsonConvert.SerializeObject(payload, Formatting.None, serialiserSettings));
                        os.Close();
                    }
                }
                else
                    req.ContentLength = 0;
            }

            try
            {
                using (System.Net.HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    if (resp == null)
                        return default(U);
                    else
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        return JsonConvert.DeserializeObject<U>(sr.ReadToEnd().Trim(), serialiserSettings);
                    }
                }
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.ProtocolError)
                {
                    switch ((int)((HttpWebResponse)we.Response).StatusCode)
                    {
                        case 400:
                        case 401:
                            throw ThrowReworkedCustomException<EX>(we);
                        case 404:
                        default:
                            throw we;
                    }
                }
                else
                {
                    throw we;
                }
            }
        }

        private static Exception ThrowReworkedCustomException<EX>(WebException we) where EX : ErrorResult
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(((HttpWebResponse)we.Response).GetResponseStream()))
            {
                string response = sr.ReadToEnd().Trim();
                ErrorResult result = JsonConvert.DeserializeObject<EX>(response);
                string message;

                if (result is OAuthErrorResult)
                    message = string.Format(
                        "The CreateSend OAuth receiver responded with the following error - {0}: {1}",
                        (result as OAuthErrorResult).error,
                        (result as OAuthErrorResult).error_description);
                else // Regular ErrorResult format.
                    message = string.Format(
                        "The CreateSend API responded with the following error - {0}: {1}", 
                        result.Code, result.Message);

                CreatesendException exception;
                if (result.Code == "121")
                    exception = new ExpiredOAuthTokenException(message);
                else
                    exception = new CreatesendException(message);

                exception.Data.Add("ErrorResponse", response);
                exception.Data.Add("ErrorResult", result);
                return exception;
            }
        }
    }

    public static class NameValueCollectionExtension
    {
        public static string ToQueryString(NameValueCollection nvc)
        {
            if (nvc != null && nvc.Count > 0)
                return "?" + string.Join("&", GetPairs(nvc));
            else
                return "";
        }

        private static string[] GetPairs(NameValueCollection nvc)
        {
            List<string> keyValuePair = new List<string>();

            foreach (string key in nvc.AllKeys)
            {
                string encodedKey = HttpUtility.UrlEncode(key) + "=";

                foreach (string value in nvc.GetValues(key))
                    keyValuePair.Add(encodedKey + HttpUtility.UrlEncode(value));
            }

            return keyValuePair.ToArray();
        }
    }
}
