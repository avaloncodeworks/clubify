using System.ComponentModel.DataAnnotations;

namespace Clubify.Data.Models
{
    public class LicenseTier
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = "";

        public int MemberCap { get; set; }

        [DataType(DataType.Currency)]
        public decimal MonthlyPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal AnnualPrice { get; set; }

        [StringLength(500)]
        public string Notes { get; set; } = "";
    }
}
