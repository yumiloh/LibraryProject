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
        List<BookModel> GetBooks();
        BookModel GetBookByID(int? bookID);
        BookModel CreateBook(BookModel book);
        int DeleteBook(int? bookID);
        int UpdateBook(BookModel book);
        int Save();
    }
}
