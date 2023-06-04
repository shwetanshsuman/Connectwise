using ConnectwiseWebApplication.Models;
using System.Net.Mail;

namespace ConnectwiseWebApplication.CommonMethods
{
    public class EmailExecution
    {
        public void SendEmail(Email email)
        {
            try
            {
                MailMessage newMail = new MailMessage();
                
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                newMail.From = new MailAddress(email.MailFrom, email.MailFromName);

                newMail.To.Add(email.MailTo);

                newMail.Subject = email.Subject;

                newMail.IsBodyHtml = true;

                newMail.Body = email.Body; /*"<h1> This is my first Templated Email in C# </h1>"*/

                // enable SSL for encryption across channels
                client.EnableSsl = true;
                client.Port = 587;
                // Provide authentication information with Gmail SMTP server to authenticate your sender account
                client.Credentials = new System.Net.NetworkCredential("shwetansh.suman@globallogic.com", "Backstreet@10");

                client.Send(newMail); // Send the constructed mail
            }
            catch
            {
            }
        }
    }
}
