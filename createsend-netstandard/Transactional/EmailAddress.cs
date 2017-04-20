using createsend_netstandard.Models;

namespace createsend_dotnet.Transactional
{
    public class EmailAddress
    {
        private readonly MailAddress mail;

        public EmailAddress(string email, string name)
        {
            mail = new MailAddress(email, name);
        }

        public EmailAddress(string email) : this(email, null)
        {
        }

        public string Email { get { return mail.Address; } }
        public string Name { get { return mail.DisplayName; } }

        public static EmailAddress FromString(string email)
        {
            return email == null ? null : new EmailAddress(email);
        }

        public static implicit operator EmailAddress(string email)
        {
            return FromString(email);
        }

        public static implicit operator string(EmailAddress email)
        {
            return ToString(email);
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