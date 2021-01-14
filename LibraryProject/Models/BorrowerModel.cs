using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BorrowerModel : UserModel
    { 
        public List<BookModel> BooksBorrowed { get; set; }
    }
}