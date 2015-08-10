using System;
using System.Collections.Specialized;
using System.Globalization;

namespace createsend_dotnet.Transactional
{
    public static class Authenticate
    {
        public static IAgencyTransactional ByAccountApiKey(string apiKey, ICreateSendOptions options = null)
        {
            if (apiKey == null) throw new ArgumentNullException("apiKey");

            return new TransactionalContext(new AccountApiKey(apiKey), options);
        }

        public static IAgencyTransactional ByAccountApiKey(string apiKey, string clientId, ICreateSendOptions options = null)
        {
            if (apiKey == null) throw new ArgumentNullException("apiKey");
            if (clientId == null) throw new ArgumentNullException("clientId");

            return new TransactionalContext(new AccountApiKey(apiKey, clientId), options);
        }

        public static ITransactional ByClientApiKey(string apiKey, ICreateSendOptions options = null)
        {
            if (apiKey == null) throw new ArgumentNullException("apiKey");

            return new TransactionalContext(new ClientApiKey(apiKey), options);
        }

        public static ITransactional ByOAuth(string accessToken, string refreshToken, string clientId, ICreateSendOptions options = null)
        {
            if (accessToken == null) throw new ArgumentNullException("accessToken");
            if (refreshToken == null) throw new ArgumentNullException("refreshToken");
            if (clientId == null) throw new ArgumentNullException("clientId");

            return new TransactionalContext(new OAuthWithClientId(accessToken, refreshToken, clientId), options);
        }

        internal static string PossiblyOptionalClientId(this CreateSendBase source, string clientId)
        {
            if (source == null) throw new ArgumentNullException("source");

            var auth = source.AuthDetails as IProvideClientId;

            if (auth != null)
            {
                clientId = clientId ?? auth.ClientId;

                if (clientId == null)
                {
                    throw new RequiredClientIdentierException();
                }
            }

            return clientId;
        }

        internal static NameValueCollection CreateQueryString(this CreateSendBase source, string clientId = null, NameValueCollection query = null)
        {
            if (source == null) throw new ArgumentNullException("source");

            query = query ?? new NameValueCollection();
            clientId = source.PossiblyOptionalClientId(query.Get("clientid") ?? clientId);
            if (clientId != null)
            {
                query["clientid"] = clientId;
            }

            return query;
        }

        internal static string Encode(this string value)
        {
            return value;
        }

        internal static string Encode(this bool value)
        {
            return Encode(value.ToString().ToLowerInvariant());
        }

        internal static string Encode(this Enum value)
        {
            return value == null ? null : Encode(value.ToString().ToLowerInvariant());
        }

        internal static string Encode(this Guid value)
        {
            return Encode(value.ToString());
        }

        internal static string Encode(this Guid? value)
        {
            return value.HasValue ? Encode(value.Value) : null;
        }

        internal static string Encode(this int value)
        {
            return Encode(value.ToString(CultureInfo.InvariantCulture));
        }

        internal static string Encode(this int? value)
        {
            return value.HasValue ? Encode(value.Value) : null;
        }

        internal static string EncodeIso8601DateOnly(this DateTime date)
        {
            return Encode(date.ToString("yyyy-MM-dd"));
        }

        internal static string EncodeIso8601DateOnly(this DateTime? date)
        {
            return date.HasValue ? EncodeIso8601DateOnly(date.Value) : null;
        }
    }
}
