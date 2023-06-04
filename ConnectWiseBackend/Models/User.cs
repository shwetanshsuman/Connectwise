using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectWiseBackend.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [ForeignKey("BranchId")]
        public int BranchId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        //additional fields
        public long PhoneNumber;
        public string Department { get; set; }
        public string JobRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

        public virtual Role Role { get; set; }
        public virtual Branch Branch { get; set; }

    }
}