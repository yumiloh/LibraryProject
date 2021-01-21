using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryProject.DataAccess;
using LibraryProject.Enums;
using LibraryProject.Models;
using LibraryProject.Repository;
using LibraryProject.ViewModels;

namespace LibraryProject.Controllers
{
    public class BorrowerController : Controller
    {
        private IBorrowerRepository BorrowerRepository { get; set; }
        private ILibraryRepository LibraryRepository { get; set; }
        public BorrowerController(IBorrowerRepository borrowerRepository, ILibraryRepository libraryRepository)
        {
            BorrowerRepository = borrowerRepository;
            LibraryRepository = libraryRepository;
        }
        
        private LoginViewModel LoggedInUser
        {
            get
            {
                return this.Session["User"] as LoginViewModel;
            }

        }

        private bool IsBorrowerLoggedIn
        {
            get
            {
                return (LoggedInUser != null && LoggedInUser.Role == UserRole.Borrower);
            }
        }

        public ActionResult Index()
        {
            // TODO: We will be doing it with [Authorize] attributes later - for now we can check manually
            if (!this.IsBorrowerLoggedIn)
            {
                return RedirectToAction("Index", "Authentication");
            }

            this.ViewBag.BorrowerName = LoggedInUser.Name;
            List<BookModel> allBooks = BorrowerRepository.GetAllBooks();

            return View(allBooks);
        }

        public ActionResult Details(int? id)
        {
            BookModel book = LibraryRepository.GetBookByID(id);
            return View(book);
        }

        [HttpGet]
        public ActionResult Borrow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModel borrowedBook = LibraryRepository.GetBookByID(id);
            if (borrowedBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowedBook);
        }

        [HttpPost, ActionName("Borrow")]
        public ActionResult BorrowConfirmed(int? id)
        {
            string borrowerEmail = LoggedInUser.Email;
            bool isBorrowed = BorrowerRepository.BorrowBooks(id,borrowerEmail);
            if (isBorrowed)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModel returnedBook = LibraryRepository.GetBookByID(id);
            if (returnedBook == null)
            {
                return HttpNotFound();
            }
            return View(returnedBook);
        }

        [HttpPost, ActionName("Return")]
        public ActionResult ReturnConfirmed(int? id)
        {
            string returnerEmail = LoggedInUser.Email;
            bool isReturned = BorrowerRepository.ReturnBooks(id, returnerEmail);
            if (isReturned)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        /*public ActionResult Login(BorrowerModel inputModel)
        {
            var borrower = borrowerRepository.AuthenticateBorrower(inputModel);

            if (borrower != null)
            {
               this.Session["BorrowerEmail"] = borrower.Email;
                this.Session["BorrowerName"] = borrower.Name;
                return RedirectToAction("Index");
            }
            else
            {
                this.Session["BorrowerEmail"] = string.Empty;
                // TODO: We will avoid using ViewBag later
                ViewBag.InvalidLogin = true;
            }
            return View();
        }*/
        [HttpGet]
        public ActionResult BorrowedBooks()
        {
            string borrowerEmail = LoggedInUser.Email;
            var borrowedBooks = BorrowerRepository.GetBorrowedBook(borrowerEmail);
            return View(borrowedBooks);
        }
        protected override void Dispose(bool disposing)
        {
            BorrowerRepository.Dispose();
            LibraryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
