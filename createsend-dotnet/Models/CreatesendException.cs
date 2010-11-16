using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class CreatesendException : Exception
    {
        public CreatesendException(string message, object additionalResultData)
        {
            ResultData = additionalResultData;
        }
        public object ResultData { get; set; }
    }
}
