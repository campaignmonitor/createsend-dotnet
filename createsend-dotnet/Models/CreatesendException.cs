using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class CreatesendException : Exception
    {
        public CreatesendException(string message) : base (message) { }
    }
}
