using System.ComponentModel.DataAnnotations;

namespace LokesNewsletterFunction.Models.DataBase
{
    public class SubscriptionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type of Subscription")]
        [StringLength(50)]
        public string TypeName { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 9999)]
        public decimal Price { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
