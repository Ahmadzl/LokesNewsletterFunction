using System.ComponentModel.DataAnnotations;

namespace LokesNewsletterFunction.Models.DataBase
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SubscriptionTypeId { get; set; }
        public SubscriptionType SubscriptionType { get; set; } = null!;

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 9999)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Subscription Started")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [Required]
        [Display(Name = "Subscription Ends")]
        [DataType(DataType.Date)]
        public DateTime Expires { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        [Required]
        public bool PaymentComplete { get; set; }
    }
}
