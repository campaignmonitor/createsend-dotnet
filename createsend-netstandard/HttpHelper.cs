using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO;
using createsend_dotnet.Transactional;
using System.Linq;

#if SUPPORTED_FRAMEWORK_VERSION
using createsend_dotnet.Transactional;
#endif

using Newtonsoft.Json;

namespace createsend_dotnet
{
    public static class HttpHelper
    {
        public const string APPLICATION_FORM_URLENCODED_CONTENT_TYPE = "application/x-www-form-urlencoded";
        public const string APPLICATION_JSON_CONTENT_TYPE = "application/json";

        public static string Delete(AuthenticationDetails auth, string path, NameValueCollection queryArguments)
        {
            return MakeRequest<string, string, ErrorResult>("DELETE", auth, path, queryArguments, null);
        }

        // NEW
        public static string Delete(AuthenticationDetails auth, string path, NameValueCollection queryArguments, string baseUri, string contentType)
        {
            return MakeRequestAsync<string, string, ErrorResult>("DELETE", auth, path, queryArguments, null, baseUri, contentType).Result;
        }

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

        public static U Get<U, EX>(
            AuthenticationDetails auth,
            string baseUri,
            string path,
            NameValueCollection queryArguments)
            where EX : ErrorResult
        {
            return MakeRequestAsync<string, U, EX>("GET", auth, path, queryArguments, null, baseUri, APPLICATION_JSON_CONTENT_TYPE).Result;
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
            return MakeRequestAsync<T, U, EX>("POST", auth, path, queryArguments, payload, baseUri, contentType).Result;
        }

        public static U Put<T, U>(AuthenticationDetails auth, string path, NameValueCollection queryArguments, T payload) where T : class
        {
            return MakeRequest<T, U, ErrorResult>("PUT", auth, path, queryArguments, payload);
        }

        // NEW
        public static U Put<T, U>(AuthenticationDetails auth, string path, NameValueCollection queryArguments, T payload, string baseUri, string contentType) where T : class
        {
            return MakeRequestAsync<T, U, ErrorResult>("PUT", auth, path, queryArguments, payload, baseUri, contentType).Result;
        }

        private static U MakeRequest<T, U, EX>(
            string method,
            AuthenticationDetails auth,
            string path,
            NameValueCollection queryArguments,
            T payload)
            where T : class
            where EX : ErrorResult
        {
            return MakeRequestAsync<T, U, EX>(method, auth, path, queryArguments,
                payload, CreateSendOptions.BaseUri, APPLICATION_JSON_CONTENT_TYPE).Result;
        }

        private static async Task<U> MakeRequestAsync<T, U, EX>(
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
            try
            {
                JsonSerializerSettings serialiserSettings = new JsonSerializerSettings();
                serialiserSettings.NullValueHandling = NullValueHandling.Ignore;
                serialiserSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                serialiserSettings.Converters.Add(new EmailAddressConverter());

                string uri = baseUri + path + NameValueCollectionExtension.ToQueryString(queryArguments);

                HttpClientHandler handler = new HttpClientHandler();
                handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;

                HttpClient client = new HttpClient(handler);

                if (auth != null)
                {
                    if (auth is OAuthAuthenticationDetails)
                    {
                        OAuthAuthenticationDetails oauthDetails = auth as OAuthAuthenticationDetails;
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + oauthDetails.AccessToken);
                    }
                    else if (auth is ApiKeyAuthenticationDetails)
                    {
                        ApiKeyAuthenticationDetails apiKeyDetails = auth as ApiKeyAuthenticationDetails;
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(
                            Encoding.GetEncoding(0).GetBytes(apiKeyDetails.ApiKey + ":x")));
                    }
                    else if (auth is BasicAuthAuthenticationDetails)
                    {
                        BasicAuthAuthenticationDetails basicDetails = auth as BasicAuthAuthenticationDetails;
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(
                            Encoding.GetEncoding(0).GetBytes(basicDetails.Username + ":" + basicDetails.Password)));
                    }
                }

                HttpContent content = null;
                HttpResponseMessage response = null;

                if (method != "GET")
                {
                    Stream s = new MemoryStream();
                    Stream requestStream = new MemoryStream();

                    if (payload != null)
                    {
                        using (System.IO.StreamWriter os = new System.IO.StreamWriter(s))
                        {
                            if (contentType == APPLICATION_FORM_URLENCODED_CONTENT_TYPE)
                                os.Write(payload);
                            else
                            {
                                string json = JsonConvert.SerializeObject(payload, Formatting.None, serialiserSettings);
                                os.Write(json);
                            }

                            await os.FlushAsync();
                            s.Seek(0, SeekOrigin.Begin);
                            await s.CopyToAsync(requestStream);
                            os.Dispose();
                        }

                        requestStream.Seek(0, SeekOrigin.Begin);
                        content = new StreamContent(requestStream);
                        response = await client.PostAsync(uri, content);
                    }
                    else
                    {
                        response = await client.PostAsync(uri, null);
                    }
                }
                else
                {
                    response = await client.GetAsync(uri);
                }

                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStreamAsync();

                    if (resp == null)
                        return default(U);

                    {
                        using (var sr = new System.IO.StreamReader(resp))
                        {
                            var type = typeof(U);
                            if (type.GetGenericTypeDefinition() == typeof(RateLimited<>))
                            {
                                var responseType = type.GenericTypeArguments[0];
                                var result = JsonConvert.DeserializeObject(sr.ReadToEnd().Trim(), responseType, serialiserSettings);
                                var status = new RateLimitStatus
                                {
                                    Credit = response.Headers.Contains("X-RateLimit-Limit") ? uint.Parse(response.Headers.GetValues("X-RateLimit-Limit").First()) : 0,
                                    Remaining = response.Headers.Contains("X-RateLimit-Remaining") ? uint.Parse(response.Headers.GetValues("X-RateLimit-Remaining").First()) : 0,
                                    Reset = response.Headers.Contains("X-RateLimit-Reset") ? uint.Parse(response.Headers.GetValues("X-RateLimit-Reset").First()) : 0
                                };
                                return (U)Activator.CreateInstance(type, result, status);
                            }
                            return JsonConvert.DeserializeObject<U>(sr.ReadToEnd().Trim(), serialiserSettings);
                        }
                    }
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        case System.Net.HttpStatusCode.Unauthorized:
                            throw ThrowReworkedCustomException<EX>(response);
                        case System.Net.HttpStatusCode.NotFound:
                        default:
                            throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

#if SUPPORTED_FRAMEWORK_VERSION
        private static uint UInt(this string value, uint defaultValue)
        {
            uint v;
            if (uint.TryParse(value, out v))
            {
                return v;
            }
            return defaultValue;
        }
#endif

        private static Exception ThrowReworkedCustomException<EX>(HttpResponseMessage messageResponse) where EX : ErrorResult
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(messageResponse.Content.ReadAsStreamAsync().Result))
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
            string url = string.Empty;
            if (nvc == null)
            {
                return url;
            }

            Dictionary<string, string> queryValues = new Dictionary<string, string>();

            foreach (string key in nvc)
            {
                if (!string.IsNullOrWhiteSpace(nvc[key]))
                {
                    queryValues.Add(key, nvc[key]);
                }
            }

            return Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(url, queryValues);
        }
    }
}