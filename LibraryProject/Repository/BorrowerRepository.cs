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
        public List<BookModel> GetAllBooks()
        {
            //List<BookModel> books = Context.Books.Where<BookModel>(x => (x.BookCopies - x.BorrowedCopies) > 0).ToList();   
            return Context.Books.ToList();
        }
        public bool BorrowBooks(int? bookID, string borrowerEmail)
        {
            BookModel book = this.FindBook(bookID);
            if (book.IsAvailable)
            {
                var borrower = Context.Borrowers.First<BorrowerModel>(x => x.Email.Equals(borrowerEmail));
                var borrowedBook = new BorrowedBookModel() { Book = book, Borrower = borrower };
                Context.BorrowedBooks.Add(borrowedBook);
                book.BorrowedCopies += 1;
                this.Save();
                return true;
            }
            return false;
        }

        public bool ReturnBooks(int? bookID, string borrowerEmail)
        {
            BookModel book = this.FindBook(bookID);
            BorrowedBookModel returnedBook = Context.BorrowedBooks.First(x => x.Book.ID.Equals(book.ID) && x.Borrower.Email.Equals(borrowerEmail));
            Context.BorrowedBooks.Remove(returnedBook);
            book.BorrowedCopies -= 1;
            this.Save();
            return true;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public UserModel FindBorrower(UserModel borrower)
        {
            BorrowerModel returnBorrower = Context.Borrowers.FirstOrDefault<BorrowerModel>(x => x.Email.Equals(borrower.Email) && x.Password.Equals(borrower.Password));


            return returnBorrower;
        }


        public List<BookModel> GetBorrowedBook(string borrowerEmail)
        {
            List<BookModel> borrowedBooks = (Context.BorrowedBooks.Where<BorrowedBookModel>(x => x.Borrower.Email.Equals(borrowerEmail)).Select(x => x.Book)).ToList();

            /*var query =( from b in borrowedBooks 
                        group b by b.Title into g
                        select new { Title = g.Key, g, Total = g.Count() }).ToList();*/

            /*List<BookModel> query = borrowedBooks
                .GroupBy(x => x.Title)
                .Select(y => new BookModel() { Title = y.Key, BorrowedCopies = y.Count()}).ToList();*/

            var query =(List<BookModel>) (from b in borrowedBooks
                         let c = new { b.ID,  b.Title, b.ISBN,  b.NumberOfPages, b.BookCopies, b.BorrowedCopies, b.IsAvailable }
                         group c by c.Title into MyGroup
                         select MyGroup);

            return borrowedBooks;
        }

        public BookModel FindBook(int? bookID)
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