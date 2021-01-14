using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Book Name")]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string ISBN { get; set; }
        public int NumberOfPages { get; set; }
        public int BookCopies { get; set; }
        public int BorrowedCopies {get; set;}
    }
}