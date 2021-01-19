using LibraryProject.DataAccess;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.ViewModels
{
    public class BookViewModel
    {
        public BookModel Book { get; private set; }
        public BookViewModel(BookModel book)
        {
            this.Book = book;
        }
        public List<BorrowerModel> Borrowers
        {
            get
            {
                LibraryContext libraryContext = new LibraryContext();
                List<BorrowerModel> borrower = (libraryContext.BorrowedBooks.Where(x => x.Book.ID == this.Book.ID).Select(x => x.Borrower)).ToList();
                List<BorrowerModel> borrowerModels = borrower.GroupBy(x => x.Name).Select(y => new BorrowerModel() { Name = y.Key, ID = y.Count() }).ToList();
                return borrowerModels;
            }
        }
    }
}