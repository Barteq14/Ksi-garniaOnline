using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int IdCategory { get; set; }
        [Required]
        [MaxLength(100)]
        public string KindOfBook { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
