using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UmbracoRaffle.Data;

namespace UmbracoRaffle.Models
{
    public class Raffle
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 1)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Firstname contains characters that can't be added to the database")]
        public string Firstname { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 1)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Firstname contains characters that can't be added to the database")]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Range(18, 150, ErrorMessage = "You must be 18+ to enter this raffle!")]
        public int Age { get; set; }
        [Required]
        [Remote("ValidateSerial", "Raffle", HttpMethod ="POST", ErrorMessage ="The serialnumber you entered is either invalid or has been entered in the raffle two times")]
        public int Number { get; set; }
    }
}
