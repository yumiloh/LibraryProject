using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Repository
{
    public interface IManagerRepository: IDisposable
    {
        List<BorrowerModel> GetBorrower();
        BorrowerModel GetBorrowerByID(int? borrowerID);
        BorrowerModel CreateBorrower(BorrowerModel borrower);
        int DeleteBorrower(int? borrowerID);
        int UpdateBorrower(BorrowerModel borrower);
        int Save();
        UserModel FindManager(UserModel manager);
    }
}
