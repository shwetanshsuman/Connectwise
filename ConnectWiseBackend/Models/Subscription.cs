using System.ComponentModel.DataAnnotations;

namespace ConnectWiseBackend.Models
{
    public class Subscription
    {


        [Key]
        public int SubscriptionId { get; set; }

        public string Description { get; set; }
        public int TimePeriodInMonths { get; set; }

        public float SubscriptionFees { get; set; }

        public bool IsActive { get; set; }



        public virtual ICollection<Company> Company { get; set; }
    }
}