using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectwiseWebApplication.Models
{
    public class Invoice
    {


        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("BranchId")]
        public int BranchId { get; set; }
        public string InvoiceTitle { get; set; }
        public string InvoiceDescription { get; set; }

        public int Status { get; set; }

        public int TotalAmount { get; set; }
        public int DueAmount { get; set; }
        public string GeneratedForEmail { get; set; }
        public int GeneratedById { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }


        public virtual Reminder Reminder { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }

        public virtual Branch Branch { get; set; }



    }

}