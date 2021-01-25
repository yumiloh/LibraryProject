using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Book")]
        public string Title { get; set; }
        [MaxLength(50)]
        [RegularExpression("^(?:ISBN(?:-13)?:?\\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\\ ]){4})[-\\ 0-9]{17}$)97[89][-\\ ]?[0-9]{1,5}[-\\ ]?[0-9]+[-\\ ]?[0-9]+[-\\ ]?[0-9]$")]
        [Required(ErrorMessage = "Please enter a valid ISBN")]
        public string ISBN { get; set; }
        [DisplayName("Number of Pages")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive value")]
        public int NumberOfPages { get; set; }
        [DisplayName("Total Book Copies")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive value")]
        public int BookCopies { get; set; }
        [DisplayName("Total Borrowed Copies")]
        public int BorrowedCopies { get; set; }
        public bool IsAvailable
        {
            get
            {
                return (BookCopies - BorrowedCopies > 0);
            }
        }
        public Book()
        {
            this.NumberOfPages = 0;
            this.BookCopies = 0;
            this.BorrowedCopies = 0;
        }
    }
}