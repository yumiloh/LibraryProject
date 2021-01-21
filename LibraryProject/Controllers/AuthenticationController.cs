using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryProject.ViewModels;
using LibraryProject.Repository;
using LibraryProject.DataAccess;
using LibraryProject.Enums;
using LibraryProject.Base;

namespace LibraryProject.Controllers
{
    public class AuthenticationController : LibraryBaseController
    {
        private IManagerRepository ManagerRepository { get; set; }
        private IBorrowerRepository BorrowerRepository { get; set; }
        public AuthenticationController(IManagerRepository managerRepository, IBorrowerRepository borrowerRepository)
        {
            this.ManagerRepository = managerRepository;
            this.BorrowerRepository = borrowerRepository;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel user)
        {
            ViewBag.Error = string.Empty;
            var databaseUser = (user.Role == UserRole.Manager) ? ManagerRepository.FindManager(user) : BorrowerRepository.FindBorrower(user);

            if (databaseUser != null)
            {
                user.Name = databaseUser.Name;
                this.CurrentUser = user;
                return RedirectToAction("Index", Convert.ToString(user.Role));
            }
            else
            {
                ViewBag.Error = "Invalid username or password.";
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            this.Session.Abandon();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            ManagerRepository.Dispose();
            BorrowerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}