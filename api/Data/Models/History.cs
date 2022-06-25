using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Data.Models
{
    public partial class History
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? CreditInKgOfCo2 { get; set; }
        public DateTime? Date { get; set; }
        [StringLength(256)]
        public string Code { get; set; }
        public int? TripId { get; set; }
        public string Data { get; set; }

        [ForeignKey("TripId")]
        [InverseProperty("Histories")]
        public virtual Trip Trip { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Histories")]
        public virtual User User { get; set; }
    }
}
