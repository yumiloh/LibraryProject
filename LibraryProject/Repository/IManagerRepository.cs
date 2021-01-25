using LibraryProject.Models;
using LibraryProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Repository
{
    public interface IManagerRepository: IDisposable
    {
        List<Borrower> GetBorrower();
        Borrower GetBorrowerByID(int? borrowerID);
        Borrower CreateBorrower(Borrower borrower);
        int DeleteBorrower(int? borrowerID);
        int UpdateBorrower(Borrower borrower);
        int Save();
        User FindManager(LoginViewModel manager);
    }
}
