using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace createsend_dotnet
{
    public class ApiKeyResult
    {
        public string ApiKey { get; set; }
    }

    public class BulkImportResults
    {
        public List<string> DuplicateEmailsInSubmission { get; set; }
        public List<ImportResult> FailureDetails { get; set; }
        public int TotalExistingSubscribers { get; set; }
        public int TotalNewSubscribers { get; set; }
        public int TotalUniqueEmailsSubmitted { get; set; }
    }

    public class ErrorResult<T> : ErrorResult
    {
        public ErrorResult() : base()
        {
        }

        public ErrorResult(ErrorResult errorResult)
        {
            Message = errorResult.Message;
            Code = errorResult.Code;
        }

        public T ResultData { get; set; }
    }

    public class ErrorResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ImportResult : ErrorResult
    {
        public string EmailAddress { get; set; }
    }

    public class OAuthErrorResult : ErrorResult
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    public class SystemDateResult
    {
        public DateTime SystemDate { get; set; }
    }
}