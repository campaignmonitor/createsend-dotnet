using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace createsend_dotnet.Transactional
{
    public class EmailAddress
    {
        public string Name { get { return mail.DisplayName; } }
        public string Email { get { return mail.Address; } }

        private readonly MailAddress mail;

        public EmailAddress(string email, string name)
        {
            mail = new MailAddress(email, name);
        }
        public EmailAddress(string email) : this(email, null) { }

        public static implicit operator EmailAddress(string email)
        {
            return FromString(email);
        }

        public static implicit operator string(EmailAddress email)
        {
            return ToString(email);
        }

        public static EmailAddress FromString(string email)
        {
            return email == null ? null : new EmailAddress(email);
        }

        public static string ToString(EmailAddress email)
        {
            return email == null ? null : email.ToString();
        }

        public override string ToString()
        {
            return mail.ToString();
        }
    }
}
