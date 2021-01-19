using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryProject.ViewModels;
using LibraryProject.Repository;
using LibraryProject.DataAccess;

namespace LibraryProject.Controllers
{
    public class AuthenticationController : Controller
    {
        private IManagerRepository managerRepository { get; set; }
        private IBorrowerRepository borrowerRepository { get; set; }
        public AuthenticationController()
        {
            managerRepository = new ManagerRepository();
            borrowerRepository = new BorrowerRepository();
        }
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel user)
        {
            if (user.Role == "Manager") //Manager
            {
                bool managerFound = managerRepository.FindManager(user);

                if (managerFound)
                {
                    this.Session["ManagerEmail"] = user.Email;
                    this.Session["ManagerName"] = user.Name;
                    return RedirectToAction("Index", "Manager");
                }
            }
            else if (user.Role == "Borrower") //Borrower
            {
                bool borrowerFound = borrowerRepository.FindBorrower(user);
                if (borrowerFound)
                {
                    this.Session["BorrowerEmail"] = user.Email;
                    this.Session["BorrowerName"] = user.Name;
                    return RedirectToAction("Index", "Borrower");
                }
            }
            else
            {
                this.Session[""] = string.Empty;
                // TODO: We will avoid using ViewBag later
                ViewBag.InvalidLogin = true;
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            managerRepository.Dispose();
            borrowerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}