using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.Models
{
    public class BookCategory
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryBook { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
