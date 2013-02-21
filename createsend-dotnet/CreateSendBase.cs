using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace createsend_dotnet
{
    public abstract class CreateSendBase
    {
        public AuthenticationDetails AuthDetails { get; set; }

        public CreateSendBase(AuthenticationDetails auth)
        {
            Authenticate(auth);
        }

        public void Authenticate(AuthenticationDetails auth)
        {
            AuthDetails = auth;
        }

        public U HttpGet<U>(string path, NameValueCollection queryArguments)
        {
            return HttpGet<U, ErrorResult>(path, queryArguments);
        }

        public U HttpGet<U, EX>(string path, NameValueCollection queryArguments)
            where EX : ErrorResult
        {
            return HttpHelper.Get<U, EX>(AuthDetails, path, queryArguments);
        }

        public U HttpPost<T, U>(string path, NameValueCollection queryArguments, T payload)
            where T : class
        {
            return HttpPost<T, U, ErrorResult>(path, queryArguments, payload);
        }

        public U HttpPost<T, U, EX>(string path, NameValueCollection queryArguments, T payload)
            where T : class
            where EX : ErrorResult
        {
            return HttpHelper.Post<T, U, ErrorResult>(AuthDetails, path, queryArguments, payload);
        }

        public U HttpPut<T, U>(string path, NameValueCollection queryArguments, T payload) where T : class
        {
            return HttpHelper.Put<T, U>(AuthDetails, path, queryArguments, payload);
        }

        public string HttpDelete(string path, NameValueCollection queryArguments)
        {
            return HttpHelper.Delete(AuthDetails, path, queryArguments);
        }
    }
}
