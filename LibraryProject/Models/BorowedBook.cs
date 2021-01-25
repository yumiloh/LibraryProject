using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BorrowedBook
    {
        public int ID { get; set; }
        public Book Book { get; set; }
        public Borrower Borrower { get; set; }
    }
}