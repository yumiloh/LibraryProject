using LibraryProject.DataAccess;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryProject.ViewModels
{
    public class BorrowerViewModel
    {
        public BorrowerModel Borrower { get; set; }
        public BorrowerViewModel(BorrowerModel borrower)
        {
            this.Borrower = borrower;
        }
        public BorrowerViewModel() { }
        [DisplayName("Borrowed Books")]
        public List<BookModel> BorrowedBooks
        {
            get
            {
                LibraryContext libraryContext = new LibraryContext();
                List<BookModel> books = (libraryContext.BorrowedBooks.Where(x => x.Borrower.Email.Equals(this.Borrower.Email)).Select(x => x.Book)).ToList();
                List<BookModel> bookModels = books.GroupBy(x => x.ID).Select(y => new BookModel() { ID = y.Key, BorrowedCopies = y.Count() }).ToList();

                return bookModels;
            }
        }

    }
}