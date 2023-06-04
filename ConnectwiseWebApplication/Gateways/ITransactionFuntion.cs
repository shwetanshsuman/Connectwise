using ConnectwiseWebApplication.Models;

namespace ConnectwiseWebApplication.Gateways
{
    public interface ITransactionFuntion
    {

        public Task<IEnumerable<Transaction>> GetTransactions();
        public Task<IEnumerable<Transaction>> GetTransactionsByInvoiceId(int id);

    }

}