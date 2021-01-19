using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BorrowedBookModel
    {
        public int ID { get; set; }
        public BookModel Book { get; set; }
        public BorrowerModel Borrower { get; set; }
    }
}