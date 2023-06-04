using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectwiseWebApplication.Models
{
    public class Transaction
    {


        [Key]
        public int TransactionId { get; set; }

        public int PayerId { get; set; }

        public int Amount { get; set; }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }

        public int Status { get; set; }

        public int ModeofPayment { get; set; }

        public DateTime DateOfPayemnt { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}