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
    //Show number of books borrowed by a certain borrower in manager detail view
    public class BorrowerViewModel
    {
        public int Count { get; set; }
        public Borrower Borrower { get; private set; }
        public BorrowerViewModel(Borrower borrower)
        {
            this.Borrower = borrower;
        }
        public BorrowerViewModel() { }
        [DisplayName("Borrowed Books")]
        public List<BookViewModel> BorrowedBooks
        {
            get
            {
                LibraryContext libraryContext = new LibraryContext();
                var booksBorrowed = (
                    from b in libraryContext.Books
                    join bb in libraryContext.BorrowedBooks on b.ID equals bb.Book.ID
                    join bor in libraryContext.Borrowers on bb.Borrower.ID equals bor.ID
                    where bor.Email.Equals(this.Borrower.Email)
                    group b by b into g
                    select new { Book = g.Key, Count = g.Count() }).ToList();
                    
                var booksBorrowedList = new List<BookViewModel>();

                foreach (var item in booksBorrowed)
                {
                    var bookViewModel = new BookViewModel(item.Book);
                    bookViewModel.Count = item.Count;

                    booksBorrowedList.Add(bookViewModel);
                }

                return booksBorrowedList;
            }
        }

    }
}