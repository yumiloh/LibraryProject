using LibraryProject.DataAccess;
using LibraryProject.Models;
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
        public ManagerRepository() : this(new LibraryContext())
        {
        }
        public ManagerRepository(LibraryContext libraryContext)
        {
            this.Context = libraryContext;
        }
        public List<BorrowerModel> GetBorrower()
        {
            
            return Context.Borrowers.ToList();
        }
        public BorrowerModel CreateBorrower(BorrowerModel borrower)
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

        public BorrowerModel GetBorrowerByID(int? borrowerID)
        {
            
            return Context.Borrowers.Find(borrowerID);
        }

        public int UpdateBorrower(BorrowerModel borrower)
        {
            Context.Entry(borrower).State = EntityState.Modified;
            return this.Save();
        }
        public int DeleteBorrower(int? borrowerID)
        {
            BorrowerModel borrower = this.GetBorrowerByID(borrowerID);
            Context.Borrowers.Remove(borrower);
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