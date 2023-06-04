namespace ConnectwiseWebApplication.Models
{
    public class RegisterationSubscription
    {
        public IEnumerable<Subscription> subscription { get; set; }
        public Registration registration { get; set; }
    }
}
