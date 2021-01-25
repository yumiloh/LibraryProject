using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryProject.DataAccess
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryConnection")
        { 
        }
        public DbSet<Book> Books { get; set;}        
        public DbSet<Manager> Managers { get; set;}        
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    }
}