using System.ComponentModel.DataAnnotations;

namespace ConnectWiseBackend.Models
{
    public class Role
    {

        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }



        public virtual ICollection<User> User { get; set; }
    }
}