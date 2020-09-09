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
        public int IBSN { get; set; }
        [Required]
        [MaxLength(55)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Autor { get; set; }
        [Required]
        [MaxLength(100)]
        public string Publisher { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PublishDate { get; set; }
        [Required]
        [Range(1,1000)]
        public int NumberOfPages { get; set; }
        public int BookCategoryID { get; set; }
        [ForeignKey("BookCategoryID")]
        public virtual BookCategory BookCategory { get; set; }
    }
}
