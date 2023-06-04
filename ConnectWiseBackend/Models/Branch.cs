using Microsoft.Identity.Client;

namespace ConnectWiseBackend.Models
{
    public class Branch
    {

        public int BranchId { get; set; }

        public int CompanyId { get; set; }

        public string BranchName { get; set; }

        public string Street { get; set; }
        public string LandMark { get; set; }


        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Pincode { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual Company Company { get; set; }


    }
}