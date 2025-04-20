namespace BLLServices.Common.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(Email email, string username);

        public void SendEmail(Email email, string username,string body);


    }
}
