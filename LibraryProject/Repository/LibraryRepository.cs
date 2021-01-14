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
        public LibraryRepository() : this(new LibraryContext())
        {
        }
        public LibraryRepository(LibraryContext libraryContext)
        {
            this.Context = libraryContext;
        }
        public List<BookModel> GetBooks()
        {
            return Context.Books.ToList();
        }
        public BookModel GetBookByID(int? bookID)
        {
            return Context.Books.Find(bookID);
        }
        public BookModel CreateBook(BookModel book)
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
            BookModel book = this.GetBookByID(bookID);
            Context.Books.Remove(book);
            return this.Save();
        }
        public int UpdateBook(BookModel book)
        {
            Context.Entry(book).State = EntityState.Modified;
            return this.Save();
        }
        public int Save()
        {
            return Context.SaveChanges();
        }
    }
}