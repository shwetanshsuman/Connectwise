using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectWiseBackend.Models
{
    public class ReminderLog
    {
        [Key]
        public int ReminderLogId { get; set; }

        [ForeignKey("ReminderId")]
        public int ReminderId { get; set; }

        public DateTime SentDate { get; set; }

        public virtual Reminder Reminder { get; set; }
    }
}
