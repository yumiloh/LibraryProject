using LibraryProject.DataAccess;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.ViewModels
{
    //Show users who borrowed a certain number of copies in book detail view
    public class BookViewModel
    {
        public int Count { get; set; }
        public Book Book { get; private set; }
        public BookViewModel(Book book = null)
        {
            this.Book = book;
        }
        public List<BorrowerViewModel> Borrowers
        {
            get
            {
                LibraryContext libraryContext = new LibraryContext();
                var borrowers = (
                    from bor in libraryContext.Borrowers
                    join bb in libraryContext.BorrowedBooks on bor.ID equals bb.Borrower.ID
                    join books in libraryContext.Books on bb.Book.ID equals books.ID
                    where books.ID.Equals(this.Book.ID)
                    group bor by bor into g
                    select new { Borrower= g.Key, Count = g.Count() }).ToList();

                var borrowersList = new List<BorrowerViewModel>();

                foreach (var item in borrowers)
                {
                    var borrowerViewModel = new BorrowerViewModel(item.Borrower);
                    borrowerViewModel.Count = item.Count;

                    borrowersList.Add(borrowerViewModel);
                }

                return borrowersList;
            }
        }
    }
}