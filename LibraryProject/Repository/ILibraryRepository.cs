using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Repository
{
    public interface ILibraryRepository: IDisposable 
    {
        List<Book> GetBooks();
        Book GetBookByID(int? bookID);
        Book CreateBook(Book book);
        int DeleteBook(int? bookID);
        int UpdateBook(Book book);
        int Save();
    }
}
