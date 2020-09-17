using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.Models
{
    public class PublishHouse
    {
        [Key]
        public int IdPublishHouse { get; set; }
        [Required]
        [MaxLength(100)]
        public string PublishHouseName { get; set; }
        [Required]
        [Range(1800,2500)]
        public int PublishmentYear { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
