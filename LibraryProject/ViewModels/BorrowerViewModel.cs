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
    //used to show number of books borrowed by a certain borrower in manager detail view
    public class BorrowerViewModel
    {
        public BorrowerModel Borrower { get; private set; }
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
                List<BookModel> bookModels = books.GroupBy(x => x.Title).Select(y => new BookModel() { Title = y.Key, BorrowedCopies = y.Count() }).ToList();

                return bookModels;
            }
        }

    }
}