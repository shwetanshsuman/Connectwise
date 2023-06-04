using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ConnectWiseBackend.Models
{
    public class Company
    {

        [Key]
        public int CompanyId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("SubscriptionId")]
        [AllowNull]
        public int SubscriptionId { get; set; }

        public bool IsDeleted { get; set; }



        public virtual ICollection<Branch> Branch { get; set; }
        public virtual Subscription Subscription { get; set; }

    }
}