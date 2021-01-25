using LibraryProject.Models;
using System;
using System.Collections.Generic;
using LibraryProject.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Repository
{
    public interface IBorrowerRepository: IDisposable
    {
        List<Book> GetAllBooks();
        bool BorrowBooks(int? bookID, string borrowerEmail);
        bool ReturnBooks(int? bookID, string borrowerEmail);
        User FindBorrower(LoginViewModel borrower);
        List<BookViewModel> GetBorrowedBook(string borrowerEmail);
       
    }
}
