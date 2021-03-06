using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Models
{
    public partial class User
    {
        public User()
        {
            Histories = new HashSet<History>();
            Trips = new HashSet<Trip>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Lastname { get; set; }
        [StringLength(50)]
        public string Firtsname { get; set; }
        [StringLength(256)]
        public string ExternalId { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<History> Histories { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
