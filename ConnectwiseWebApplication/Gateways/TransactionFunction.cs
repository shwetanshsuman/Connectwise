using Azure;
using ConnectwiseWebApplication.Models;
using Newtonsoft.Json;
using Syncfusion.EJ2.Navigations;

namespace ConnectwiseWebApplication.Gateways
{
    public class TransactionFunction : ITransactionFuntion
    {
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            IEnumerable<Transaction> transactions;
            using (var client=new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:7266/api/Transactions/"))
                {
                    var stringresponse = await response.Content.ReadAsStringAsync();
                    transactions = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(stringresponse);
                }

                return transactions;
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByInvoiceId(int id)
        {
            IEnumerable<Transaction> transactions;


            transactions =await GetTransactions();
            transactions = transactions.Where(x => x.InvoiceId == id);
            return transactions;
        }
    }
}
