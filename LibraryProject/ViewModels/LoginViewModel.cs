using LibraryProject.Enums;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.ViewModels
{
    public class LoginViewModel : UserModel
    {
        public UserRole Role { get; set; }
    }
}
