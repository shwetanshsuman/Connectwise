using ConnectWiseBackend.Models;
using ConnectwiseWebApplication.Models;
using Invoice = ConnectwiseWebApplication.Models.Invoice;

namespace ConnectwiseWebApplication.Gateways
{
    public interface IInvoiceFunctions
    {

        public Task<ReminderForm> CreateInvoice(InvoiceForm invoiceForm, HttpContext httpContext);
        public Task<int> CreateInvoiceForSubscription(InvoiceForm invoiceForm,int userid, HttpContext httpContext);

        public Task<int> CreateReminder(ReminderForm reminderform,HttpContext httpContext);

        public Task<IEnumerable<Invoice>> GetInvoices();
        public Task<IEnumerable<Invoice>> GetInvoicesByCompanyID(HttpContext httpContext);
        public Task<IEnumerable<Invoice>> GetInvoicesByuserID(int id);
        public Task<Invoice> GetInvoiceById(int id);

        public Task<int> InvoiceUserPayment(PaymentModel paymentModel,Invoice invoice, HttpContext httpContext);
    }
}