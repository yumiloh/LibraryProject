using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Repository
{
    public interface IBorrowerRepository: IDisposable
    {
        List<BookModel> GetAllBooks();
        bool BorrowBooks(int? bookID, string borrowerEmail);
        bool ReturnBooks(int? bookID, string borrowerEmail);
        UserModel FindBorrower(UserModel borrower);
        List<BookModel> GetBorrowedBook(string borrowerEmail);
       
    }
}
