using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Models
{
    public partial class Trip
    {
        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Label { get; set; }
        [StringLength(256)]
        public string Departure { get; set; }
        [StringLength(256)]
        public string Arrival { get; set; }
        public bool? IsRoundTrip { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Trips")]
        public virtual User User { get; set; }
    }
}
