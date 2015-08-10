
namespace createsend_dotnet.Transactional
{
    public sealed class RateLimited<T>
    {
        public T Response { get; private set; }
        public RateLimitStatus RateLimit { get; private set; }

        public RateLimited(T response, RateLimitStatus rateLimit)
        {
            Response = response;
            RateLimit = rateLimit;
        }
    }
}
