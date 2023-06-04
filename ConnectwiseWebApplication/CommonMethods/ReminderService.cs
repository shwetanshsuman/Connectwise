using ConnectwiseWebApplication.Controllers;
using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ConnectwiseWebApplication.CommonMethods
{
	public class ReminderService:BackgroundService
	{
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				ReminderFunctions reminderFunctions = new ReminderFunctions();
				IEnumerable<Reminder> reminders = await reminderFunctions.GetPendingInvoiceReminders();
                InvoiceFunction invoiceFunction=new InvoiceFunction();

                // Check each reminder
                foreach (Reminder reminder in reminders)
				{
					if (reminder.UpdatedDate.AddDays(reminder.Frequency) < DateTime.Now)
					{
						Invoice invoice = await invoiceFunction.GetInvoiceById(reminder.InvoiceId);
                        
                        string redirectToLogin = "https://localhost:7081/";
                        MasterEmail masterEmail;
                        EmailFunction emailFunction = new EmailFunction();
                        UserFunctions userFunctions = new UserFunctions();

                        Email email = new Email()
                        {
                            MailFrom = "connectwisetestfunction@gmail.com",
                            MailFromName = "Connectwise Admin",
                            MailTo = invoice.GeneratedForEmail
                        };

                        IEnumerable<User> users = await userFunctions.GetUsers();
						User user = users.FirstOrDefault(x => x.UserName == invoice.GeneratedForEmail);

						if(user == null)
						{
                            string payLink = "https://localhost:7081/Unauth/payment/" + reminder.InvoiceId;
                            masterEmail = await emailFunction.GetEmailById(3);
                            string body = masterEmail.EmailBody;
                            body = String.Format(body, "Connectwise admin", invoice.InvoiceTitle, invoice.InvoiceTitle, invoice.TotalAmount, invoice.DueAmount, payLink, redirectToLogin);
							email.Body = body;
						}
						else
						{
                            masterEmail = await emailFunction.GetEmailById(4);
                            string body = masterEmail.EmailBody;
                            body = String.Format(body, "Connectwise admin", invoice.InvoiceTitle, invoice.InvoiceTitle, invoice.DueDate, redirectToLogin);
                            email.Body = body;
                        }
						email.Subject =masterEmail.EmailSubject;
						
                        EmailExecution emailExecution = new EmailExecution();
                        emailExecution.SendEmail(email);
                        ReminderLog reminderLog = new ReminderLog()
						{
							ReminderId=reminder.ReminderId,
                            SentDate = DateTime.Now
                        };
						if(await reminderFunctions.CreateReminderLog(reminderLog))
						{
							reminder.UpdatedDate = DateTime.Now;
							reminderFunctions.UpdateDate(reminder);
						}
					}
				}
				// Wait for a specific interval before checking again
				await Task.Delay(TimeSpan.FromHours(5), stoppingToken);
			}
		}

	}

}
