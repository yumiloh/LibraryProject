using LibraryProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Base
{
    public class LibraryBaseController : Controller
    {
        private LoginViewModel _CurrentUser { get; set; }

        protected bool IsBorrowerLoggedIn
        {
            get
            {
                return this.CurrentUser?.Role == Enums.UserRole.Borrower;
            }
        }
        protected bool IsManagerLoggedIn
        {
            get
            {
                return this.CurrentUser?.Role == Enums.UserRole.Manager;
            }
        }

        protected LoginViewModel CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                {
                    _CurrentUser = (this.Session["LoggedInUser"] as LoginViewModel);
                }

                return _CurrentUser;
            }
            set
            {
                this.Session["LoggedInUser"] = value;
                this.Session["UserRole"] = value.Role;
            }
        }
    }
}