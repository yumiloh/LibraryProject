using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryProject.Base;
using LibraryProject.DataAccess;
using LibraryProject.Enums;
using LibraryProject.Models;
using LibraryProject.Repository;
using LibraryProject.ViewModels;

namespace LibraryProject.Controllers
{
    public class BorrowerController : LibraryBaseController
    {
        private IBorrowerRepository BorrowerRepository { get; set; }
        private ILibraryRepository LibraryRepository { get; set; }
        public BorrowerController(IBorrowerRepository borrowerRepository, ILibraryRepository libraryRepository)
        {
            BorrowerRepository = borrowerRepository;
            LibraryRepository = libraryRepository;
        }

        public ActionResult Index()
        {
            // TODO: We will be doing it with [Authorize] attributes later - for now we can check manually
            if (!this.IsBorrowerLoggedIn)
            {
                return RedirectToAction("Index", "Authentication");
            }
            this.ViewBag.UserName = this.CurrentUser.Name;
            List<Book> allBooks = BorrowerRepository.GetAllBooks();
            ViewBag.BorrowedBooks = BorrowBooks();
            return View(allBooks);
        }

        public ActionResult Details(int? id)
        {
            Book book = LibraryRepository.GetBookByID(id);
            return View(book);
        }

        [HttpGet]
        public ActionResult Borrow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book borrowedBook = LibraryRepository.GetBookByID(id);
            if (borrowedBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowedBook);
        }

        [HttpPost, ActionName("Borrow")]
        public ActionResult BorrowConfirmed(int? id)
        {
            string borrowerEmail = this.CurrentUser?.Email;
            bool isBorrowed = BorrowerRepository.BorrowBooks(id, borrowerEmail);
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
            Book returnedBook = LibraryRepository.GetBookByID(id);
            if (returnedBook == null)
            {
                return HttpNotFound();
            }
            return View(returnedBook);
        }

        [HttpPost, ActionName("Return")]
        public ActionResult ReturnConfirmed(int? id)
        {
            bool isReturned = BorrowerRepository.ReturnBooks(id, this.CurrentUser?.Email);

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

        [HttpGet]
        public ActionResult BooksBorrowed()
        {
            List<BookViewModel> borrowedBooks = BorrowBooks();
            return View(borrowedBooks);
        }

        protected override void Dispose(bool disposing)
        {
            BorrowerRepository.Dispose();
            LibraryRepository.Dispose();
            base.Dispose(disposing);
        }

        public List<BookViewModel> BorrowBooks()
        {
            string borrowerEmail = this.CurrentUser?.Email;
            return BorrowerRepository.GetBorrowedBook(borrowerEmail);
        }
    }
}
