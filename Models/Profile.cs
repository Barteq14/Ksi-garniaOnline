using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.Models
{
    public class Profile
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string SurName { get; set; }
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Required]
        [MaxLength(30)]
        public string ZipCode { get; set; }
        [Required]
        [MaxLength(30)]
        public string Street { get; set; }
        [Required]
        [MaxLength(30)]
        public string Number { get; set; }

    }
}
