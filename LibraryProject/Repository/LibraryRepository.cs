using LibraryProject.DataAccess;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryProject.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext Context;
        public LibraryRepository(LibraryContext libraryContext)
        {
            this.Context = libraryContext;
        }

        public List<Book> GetBooks()
        {
            return Context.Books.ToList();
        }

        public Book GetBookByID(int? bookID)
        {
            return Context.Books.Find(bookID);
        }

        public Book CreateBook(Book book)
        {
            var bookModel = Context.Books.Add(book);
            var saveResult = this.Save();
            if (saveResult > 0)
            {
                return bookModel;
            }
            else
            {
                return null;
            }
        }

        public int DeleteBook(int? bookID)
        {
            //TODO: need to be updated. Once a book get deleted, if it's borrowed by a user, the entry is deleted too 
            Book book = this.GetBookByID(bookID);
            Context.Books.Remove(book);
            return this.Save();
        }

        public int UpdateBook(Book book)
        {
            Context.Entry(book).State = EntityState.Modified;
            return this.Save();
        }

        public int Save()
        {
            return Context.SaveChanges();
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