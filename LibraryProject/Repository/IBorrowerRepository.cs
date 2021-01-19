using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Repository
{
    interface IBorrowerRepository: IDisposable
    {
        List<BookModel> GetAvailableBooks();
        bool BorrowBooks(int? bookID, string borrowerEmail);
        bool ReturnBooks(int? bookID, string borrowerEmail);
        BorrowerModel AuthenticateBorrower(BorrowerModel borrower);
        List<BookModel> GetBorrowedBook(string borrowerEmail);
       
    }
}
