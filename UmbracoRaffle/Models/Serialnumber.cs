using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UmbracoRaffle.Models
{
    public class Serialnumber
    {
        [Key]
        public int Number { get; set; }
    }
}
