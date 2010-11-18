using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class CreatesendException : Exception
    {
        public CreatesendException(string message) : base (message) { }

        public CreatesendException(string message, string responseData)
            : base(message)
        {
            ResponseData = responseData;
        }

        public string ResponseData { get; set; }
    }
}
