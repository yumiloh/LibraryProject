using LibraryProject.DataAccess;
using LibraryProject.Models;
using LibraryProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryProject.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly LibraryContext Context;
        public ManagerRepository(LibraryContext libraryContext)
        {
            this.Context = libraryContext;
        }

        public List<Borrower> GetBorrower()
        {

            return Context.Borrowers.ToList();
        }

        public Borrower CreateBorrower(Borrower borrower)
        {
            var borrowerModel = Context.Borrowers.Add(borrower);
            var saveResult = this.Save();
            if (saveResult == 1)
            {
                return borrowerModel;
            }
            else
            {
                return null;
            }
        }

        public Borrower GetBorrowerByID(int? borrowerID)
        {
            return Context.Borrowers.Find(borrowerID);
        }

        public int UpdateBorrower(Borrower borrower)
        {
            Context.Entry(borrower).State = EntityState.Modified;
            return this.Save();
        }

        public int DeleteBorrower(int? borrowerID)
        {
            Borrower borrower = this.GetBorrowerByID(borrowerID);
            
            Context.Borrowers.Remove(borrower);

            return this.Save();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public User FindManager(LoginViewModel manager)
        {
            return Context.Managers.FirstOrDefault<Manager>(x => x.Email.Equals(manager.Email) && x.Password.Equals(manager.Password));
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