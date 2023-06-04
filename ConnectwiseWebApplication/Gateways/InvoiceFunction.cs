using ConnectwiseWebApplication.CommonMethods;
using ConnectwiseWebApplication.Models;
using Newtonsoft.Json;
using ConnectwiseWebApplication.Controllers;

namespace ConnectwiseWebApplication.Gateways
{
    public class InvoiceFunction : IInvoiceFunctions
    {
        public async Task<ReminderForm> CreateInvoice(InvoiceForm invoiceForm, HttpContext httpContext)
        {

            ReminderForm reminder = new ReminderForm();//its an null object  instance which i am using to pqass null values on the false condition
            UserFunctions UserFunctions = new UserFunctions();
            IEnumerable<User> users;
            users = await UserFunctions.GetUsers();
            User user = new User();
            try
            {//int branchid = user.BranchId;
                int? id = Convert.ToInt32(httpContext.Request.Cookies["user_Id"].ToString());
                UserFunctions userFunctions = new UserFunctions();
                User useradmin = await userFunctions.GetUserById(id);
                int branchid = useradmin.BranchId;
                //string companyEmail;

                //user = await UserFunctions.GetUserById(id);

                Invoice invoice = new Invoice()
                {
                    InvoiceTitle = invoiceForm.InvoiceTitle,
                    InvoiceDescription = invoiceForm.InvoiceDescription,
                    BranchId = branchid,
                    CreatedDate = DateTime.UtcNow,
                    DueDate = invoiceForm.DueDate,
                    GeneratedById = (int)id,
                    GeneratedForEmail = invoiceForm.GeneratedForEmail,
                    DueAmount = invoiceForm.TotalAmount * invoiceForm.TotalQuantity,
                    TotalAmount = invoiceForm.TotalAmount * invoiceForm.TotalQuantity,
                    Status = 0

                };

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:7266/api/");
                    var postTask = client.PostAsJsonAsync<Invoice>("Invoices", invoice);
                    postTask.Wait();
                    var result = postTask.Result;
                    var res = result.Content.ReadAsStringAsync().Result;
                    Invoice InvoiceData = JsonConvert.DeserializeObject<Invoice>(res);
                    var reminderForm = new ReminderForm()
                    {
                        //frequency = invoiceForm.ReminderFrequency,
                        InvoiceId = InvoiceData.InvoiceId,
                    };
                    if (result.IsSuccessStatusCode)
                    {
                        user = users.FirstOrDefault(x => x.UserName == invoiceForm.GeneratedForEmail);
                        MasterEmail masterEmail = new MasterEmail();
                        EmailFunction emailFunction = new EmailFunction();
                        Email email = new Email()
                        {
                            MailFrom = "connectwisetestfunction@gmail.com",
                            MailFromName = "Connectwise Admin",
                            MailTo = invoiceForm.GeneratedForEmail,

                        };
                        if (user == null)
                        {
                            masterEmail = await emailFunction.GetEmailById(3);
                            string PayLink = "https://localhost:7081/Unauth/payment/" + InvoiceData.InvoiceId;
                            string redirectToLogin = "https://localhost:7081/";
                            string body = masterEmail.EmailBody;
                            body = String.Format(body, "Connectwise admin", invoice.InvoiceTitle, invoice.InvoiceTitle, invoice.TotalAmount, invoice.DueAmount, PayLink, redirectToLogin);

                            email.Subject = masterEmail.EmailSubject;
                            email.Body = body;
                            EmailExecution emailExecution = new EmailExecution();
                            emailExecution.SendEmail(email);
                            return reminderForm;
                        }
                        else
                        {
                            masterEmail = await emailFunction.GetEmailById(4);
                            string PayLink = "https://localhost:7081/Unauth/payment/" + invoice.InvoiceId;
                            string redirectToLogin = "https://localhost:7081/";
                            string body = masterEmail.EmailBody;

                            body = String.Format(body, "Connectwise admin", invoice.InvoiceTitle, invoice.InvoiceTitle, invoice.DueDate, redirectToLogin);

                            email.Subject = masterEmail.EmailSubject;
                            email.Body = body;
                            EmailExecution emailExecution = new EmailExecution();
                            emailExecution.SendEmail(email);

                            return reminderForm;
                        }
                    }
                    else
                    {

                        return reminder;
                    }

                }

            }
            catch (Exception ex)
            {
                return reminder;
            }


        }

        public async Task<int> CreateReminder(ReminderForm reminderform, HttpContext httpContext)
        {
            {
                Reminder reminder = new Reminder()
                {
                    Frequency = 1,
                    InvoiceId = reminderform.InvoiceId,
                    UpdatedDate = DateTime.Now

                };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7266/api/");

                    var postTask = client.PostAsJsonAsync<Reminder>("Reminders", reminder);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                }
            }
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            Invoice invoice;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Invoices/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    invoice = JsonConvert.DeserializeObject<Invoice>(apiResponse);
                }
            }
            return invoice;
        }

        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            IEnumerable<Invoice> invoices;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Invoices"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    invoices = JsonConvert.DeserializeObject<List<Invoice>>(apiResponse);
                }
            }
            return invoices;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByCompanyID(HttpContext httpContext)
        {
            IEnumerable<Invoice> InvoiceList = null;

            try
            {
                InvoiceList = await GetInvoices();
                int? id = Convert.ToInt32(httpContext.Request.Cookies["user_Id"].ToString());
                InvoiceList = InvoiceList.Where(x => x.GeneratedById == id);

            }
            catch (Exception ex)
            {

            }
            return InvoiceList;

        }

        public async Task<int> CreateInvoiceForSubscription(InvoiceForm invoiceForm, int userid, HttpContext httpContext)
        {
            UserFunctions userFunctions = new UserFunctions();
            User user = await userFunctions.GetUserById(userid);
            int branchid = user.BranchId;


            Invoice invoice = new Invoice()
            {

                BranchId = branchid,
                CreatedDate = DateTime.UtcNow,
                DueDate = invoiceForm.DueDate,
                GeneratedById = (int)userid,
                GeneratedForEmail = invoiceForm.GeneratedForEmail,
                DueAmount = 0,
                TotalAmount = invoiceForm.TotalAmount,
                Status = 1,
                InvoiceTitle = "Subscription"


            };

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var postTask = client.PostAsJsonAsync<Invoice>("Invoices", invoice);
                postTask.Wait();
                var result = postTask.Result;
                var res = result.Content.ReadAsStringAsync().Result;
                Invoice InvoiceData = JsonConvert.DeserializeObject<Invoice>(res);
                return InvoiceData.InvoiceId;

            }
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByuserID(int id)
        {
            IEnumerable<Invoice> invoice;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Userinvoice/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    invoice = JsonConvert.DeserializeObject<IEnumerable<Invoice>>(apiResponse);
                }
            }
            return invoice;
        }

        public async Task<int> InvoiceUserPayment(PaymentModel paymentModel, Invoice invoice, HttpContext httpContext)
        {
            int? id;
            try
            {
                id = Convert.ToInt32(httpContext.Request.Cookies["user_Id"].ToString());
            }
            catch (Exception ex)
            {
                id = 0;
            }
            int dueamount = invoice.DueAmount - paymentModel.totalAmount;
            int duestatus;
            if (dueamount > 0)
            {
                duestatus = 0;
            }
            else
            {
                duestatus = 1;
            }
            Invoice invoicedata = new Invoice()
            {
                BranchId = invoice.BranchId,
                CreatedDate = invoice.CreatedDate,
                DueDate = invoice.DueDate,
                GeneratedById = invoice.GeneratedById,
                GeneratedForEmail = invoice.GeneratedForEmail,
                InvoiceDescription = invoice.InvoiceDescription,
                InvoiceId = invoice.InvoiceId,
                InvoiceTitle = invoice.InvoiceTitle,
                Status = duestatus,
                TotalAmount = invoice.TotalAmount,
                DueAmount = dueamount,

            };


            using (var client = new HttpClient())
            {



                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var puttask = client.PutAsJsonAsync("Invoices/" + invoice.InvoiceId, invoicedata);
                puttask.Wait();
                if (puttask.Result.IsSuccessStatusCode)
                {
                    SubscriptionFunctions subscriptionFunctions = new SubscriptionFunctions();
                    bool res = subscriptionFunctions.UpdateTransaction(invoice.InvoiceId, (int)id, paymentModel.totalAmount, httpContext);
                    if (res)
                    {

                        return 1;
                    }

                }

            }


            return -1;
        }
    }
}