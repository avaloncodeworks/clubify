using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clubify.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Membership is required.")]
        public int MembershipId { get; set; }

        [ForeignKey("MembershipId")]
        public Membership Membership { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 100000, ErrorMessage = "Amount must be greater than 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment date is required.")]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Payment method is required.")]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = "Manual";

        [StringLength(255)]
        public string Notes { get; set; }
    }
}
