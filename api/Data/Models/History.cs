using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Models
{
    public partial class History
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int CreditInKgOfCo2 { get; set; }
        public DateTime? Date { get; set; }
        [StringLength(256)]
        public string Code { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Histories")]
        public virtual User User { get; set; }
    }
}
