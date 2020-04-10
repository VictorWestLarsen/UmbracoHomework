using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UmbracoRaffle.Models
{
    public class Raffle
    {
        [Key]
        public int ID { get; set; }
        [StringLength(60, MinimumLength =1)]
        public string Firstname { get; set; }
        [StringLength(60, MinimumLength = 1)]
        public string Lastname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Range(18, 150, ErrorMessage = "You must be 18+ to enter this raffle!")]
        public string Age { get; set; }
        [Range(1,34000)]
        public int Serialnumber { get; set; }
    }
}
