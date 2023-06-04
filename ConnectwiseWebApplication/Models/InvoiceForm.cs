namespace ConnectwiseWebApplication.Models
{
    public class InvoiceForm
    {
        public string InvoiceTitle { get; set; }
        public string InvoiceDescription { get; set; }

        public string GeneratedForEmail { get; set; }
        public DateTime DueDate { get; set; }

        public int TotalAmount { get; set; }
        public int ReminderFrequency { get; set; }
        public int TotalQuantity { get; set; }
    }
}
