using ConnectwiseWebApplication.Models;
using Newtonsoft.Json;

namespace ConnectwiseWebApplication.Gateways
{
    public class SubscriptionFunctions : ISubscriptionFunctions
    {
        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            IEnumerable<Subscription> subscriptions;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Subscriptions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    subscriptions = JsonConvert.DeserializeObject<List<Subscription>>(apiResponse);
                }
            }
            return subscriptions;
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            Subscription subscription;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Subscriptions/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    subscription = JsonConvert.DeserializeObject<Subscription>(apiResponse);
                }
            }
            return subscription;
        }

        public bool UpdateTransaction(int invoiceid,int userid, int totalamount, HttpContext httpContext)
        {

            Transaction Transaction = new Transaction()
            {
                Amount = totalamount,
                DateOfPayemnt = DateTime.Now,
                InvoiceId = invoiceid,
                ModeofPayment = 1,
                PayerId = userid,
                Status = 1,


            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var postTask = client.PostAsJsonAsync<Transaction>("Transactions", Transaction);
                postTask.Wait();
                if (postTask.Result.IsSuccessStatusCode)
                {

                    return true;

                }

            }

            return false;


        }

        public async Task<bool> SubscriptionPurchase(RegistrationPayment registrationPayment,int userid, string data, int totalamount, HttpContext httpContext)
        {

            var registration = JsonConvert.DeserializeObject<Registration>(data);
            registrationPayment.Registration = registration;
            // here i am planning to do payment done operation
            InvoiceForm invoiceForm = new InvoiceForm()
            {
                DueDate = DateTime.Now,
                GeneratedForEmail = registration.companyEmail,
                ReminderFrequency = 1,
                TotalAmount = totalamount
            };
            InvoiceFunction invoiceFunction = new InvoiceFunction();
            int invoiceId = await invoiceFunction.CreateInvoiceForSubscription(invoiceForm, userid, httpContext);
            if (invoiceId != -1)
            {
                SubscriptionFunctions subscriptionFunctions = new SubscriptionFunctions();
                bool transactstatus = subscriptionFunctions.UpdateTransaction(invoiceId,userid, totalamount, httpContext);
                if (transactstatus)
                {
                    return true;
                }

            }
                
                return false;
        }
    }
}
