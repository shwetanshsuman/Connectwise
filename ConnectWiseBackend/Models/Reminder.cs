using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectWiseBackend.Models
{
    public class Reminder
    {

        [Key]
        public int ReminderId { get; set; }


        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int Frequency { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual ICollection<ReminderLog> ReminderLog { get; set; }    

    }
}