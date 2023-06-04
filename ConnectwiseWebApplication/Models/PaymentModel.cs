using System.ComponentModel.DataAnnotations;

namespace ConnectwiseWebApplication.Models
{
    public class PaymentModel
    {
        [Required]
        
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Please enter a valid credit card number.")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Please enter a card expiration date.")]
        [Display(Name = "Card Expiration")]
        [DataType(DataType.Date)]
        public string CardExpiration { get; set; }

        [RegularExpression(@"^\d{3}$", ErrorMessage = "Please enter a valid CVV.")]
        public string CVV { get; set; }
        [StringLength(100)]
        public string CardHolderName { get; set; }

        public int totalAmount { get; set; }

       

    }
}
