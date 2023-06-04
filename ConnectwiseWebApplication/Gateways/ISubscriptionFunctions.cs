using ConnectwiseWebApplication.Models;

namespace ConnectwiseWebApplication.Gateways
{
    public interface ISubscriptionFunctions
    {
        public Task<IEnumerable<Subscription>> GetSubscriptions();
        public  Task<Subscription> GetSubscriptionById(int id);
        public bool UpdateTransaction(int invoiceid,int userid, int totalamount, HttpContext httpContext);
        public Task<bool> SubscriptionPurchase(RegistrationPayment registrationPayment,int useid,string data, int totalamount,HttpContext httpContext);
    }
}
