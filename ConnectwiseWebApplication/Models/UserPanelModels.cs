namespace ConnectwiseWebApplication.Models
{
    public class UserPanelModels
    {
        public IEnumerable<Invoice>  CompletedInvoice { get; set; }
        public IEnumerable<Invoice>  PendingInvoice { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
