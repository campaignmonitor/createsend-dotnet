using System;
using System.Runtime.Serialization;

namespace createsend_dotnet.Transactional
{
    public class RequiredClientIdentierException : Exception
    {
        private const string defaultMessage =
            "ClientId has not been provided. Note to agencies: If you are using an account API key, this is required as you need to specify the client";

        public RequiredClientIdentierException()
            : base(defaultMessage)
        {
        }

        public RequiredClientIdentierException(string message)
            : base(message)
        {
        }

        public RequiredClientIdentierException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public RequiredClientIdentierException(Exception exception)
            : base(defaultMessage, exception)
        {
        }
    }
}