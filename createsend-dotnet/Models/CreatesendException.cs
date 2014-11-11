using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class ExpiredOAuthTokenException : CreatesendException
    {
        public ExpiredOAuthTokenException(string message) : base(message) { }
    }

    public class CreatesendException : Exception
    {
        public ErrorResult Error { get { return Data["ErrorResult"] as ErrorResult; } }

        public CreatesendException(string message) : base (message) { }
    }
}
