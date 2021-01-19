using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryProject.DataAccess;
using LibraryProject.Models;
using LibraryProject.Repository;
using LibraryProject.ViewModels;

namespace LibraryProject.Controllers
{
    public class BorrowerController : Controller
    {
        private IBorrowerRepository borrowerRepository { get; set; }
        private ILibraryRepository libraryRepository { get; set; }
        public BorrowerController()
        {
            borrowerRepository = new BorrowerRepository();
            libraryRepository = new LibraryRepository();
        }
        
        public ActionResult Index()
        {
            // TODO: We will be doing it with [Authorize] attributes later - for now we can check manually
            if (this.Session["BorrowerEmail"] == null)
            {
                return RedirectToAction("Login");
            }
            this.ViewBag.BorrowerName = this.Session["BorrowerName"];
            List<BookModel> availableBooks = borrowerRepository.GetAvailableBooks();
            return View(availableBooks);
        }

        public ActionResult Details(int? id)
        {
            BookModel book = libraryRepository.GetBookByID(id);
            return View(book);
        }

        [HttpGet]
        public ActionResult Borrow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModel borrowedBook = libraryRepository.GetBookByID(id);
            if (borrowedBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowedBook);
        }

        [HttpPost, ActionName("Borrow")]
        public ActionResult BorrowConfirmed(int? id)
        {
            string borrowerEmail = (string) this.Session["BorrowerEmail"];
            bool isBorrowed = borrowerRepository.BorrowBooks(id,borrowerEmail);
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
            BookModel returnedBook = libraryRepository.GetBookByID(id);
            if (returnedBook == null)
            {
                return HttpNotFound();
            }
            return View(returnedBook);
        }

        [HttpPost, ActionName("Return")]
        public ActionResult ReturnConfirmed(int? id)
        {
            string returnerEmail = (string)this.Session["BorrowerEmail"];
            bool isReturned = borrowerRepository.ReturnBooks(id, returnerEmail);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(BorrowerModel inputModel)
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
        }
        [HttpGet]
        public ActionResult BorrowedBooks()
        {
            string borrowerEmail = (string)this.Session["BorrowerEmail"];
            var borrowedBooks = borrowerRepository.GetBorrowedBook(borrowerEmail);
            return View(borrowedBooks);
        }
        protected override void Dispose(bool disposing)
        {
            borrowerRepository.Dispose();
            libraryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
