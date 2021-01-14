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
        public DbSet<BookModel> Books { get; set;}        
        public DbSet<ManagerModel> Managers { get; set;}        
        public DbSet<BorrowerModel> Borrowers { get; set; }
    }
}