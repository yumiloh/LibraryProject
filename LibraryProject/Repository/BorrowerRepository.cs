using LibraryProject.DataAccess;
using LibraryProject.Models;
using LibraryProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Repository
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly LibraryContext Context;

        public BorrowerRepository(LibraryContext libraryContext)
        {
            this.Context = libraryContext;
        }
        public List<Book> GetAllBooks()
        {
            return Context.Books.ToList();
        }
        public bool BorrowBooks(int? bookID, string borrowerEmail)
        {
            Book book = this.FindBook(bookID);
            if (book.IsAvailable)
            {
                var borrower = Context.Borrowers.First<Borrower>(x => x.Email.Equals(borrowerEmail));
                var borrowedBook = new BorrowedBook() { Book = book, Borrower = borrower };
                Context.BorrowedBooks.Add(borrowedBook);
                book.BorrowedCopies += 1;
                this.Save();
                return true;
            }
            return false;
        }

        public bool ReturnBooks(int? bookID, string borrowerEmail)
        {
            Book book = this.FindBook(bookID);
            BorrowedBook returnedBook = Context.BorrowedBooks.First(x => x.Book.ID.Equals(book.ID) && x.Borrower.Email.Equals(borrowerEmail));
            Context.BorrowedBooks.Remove(returnedBook);
            book.BorrowedCopies -= 1;
            this.Save();
            return true;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public User FindBorrower(LoginViewModel borrower)
        {
            Borrower returnBorrower = Context.Borrowers.FirstOrDefault<Borrower>(x => x.Email.Equals(borrower.Email) && x.Password.Equals(borrower.Password));
            return returnBorrower;
        }


        public List<BookViewModel> GetBorrowedBook(string borrowerEmail)
        {
            var booksBorrowed = (  from b in Context.Books
                                   join bb in Context.BorrowedBooks on b.ID equals bb.Book.ID
                                   join bor in Context.Borrowers on bb.Borrower.ID equals bor.ID
                                   where bor.Email.Equals(borrowerEmail)
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

        public Book FindBook(int? bookID)
        {
            return Context.Books.Find(bookID);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}