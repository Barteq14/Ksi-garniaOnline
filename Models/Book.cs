using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.Models
{
    public class Book
    {
        [Key]
        [Required]
        public int IdBook { get; set; }
        [Required]
        [MaxLength(55)]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
        [Required]
        [MaxLength(999999999)]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string Binding { get; set; }
        [Required]
        public string IBSN { get; set; }
        [Required]
        [MaxLength(100)]
        public int BookCategoryID { get; set; }
        [ForeignKey("BookCategoryID")]
        public virtual BookCategory BookCategory { get; set; }

        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public virtual Author Author { get; set; }

        public int PublishHouseID { get; set; }
        [ForeignKey("PublishHouseID")]
        public virtual PublishHouse PublishHouse { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}
