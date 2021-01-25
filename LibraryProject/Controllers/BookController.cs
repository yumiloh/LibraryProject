using LibraryProject.Base;
using LibraryProject.Enums;
using LibraryProject.Models;
using LibraryProject.Repository;
using LibraryProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class BookController : LibraryBaseController
    {
        private ILibraryRepository LibraryRepository { get; set; }
        public BookController(ILibraryRepository libraryRepository)
        {
            this.LibraryRepository = libraryRepository;
        }

        public ActionResult Index()
        {
            if (!this.IsManagerLoggedIn)
            {
                return RedirectToAction("Index", "Authentication");
            }
            List<Book> books = LibraryRepository.GetBooks();
            ViewBag.IsManager = this.IsManagerLoggedIn;
            return View(books);
        }

        public ActionResult Details(int? id)
        {
            Book book = LibraryRepository.GetBookByID(id);
            var bookViewModel = new BookViewModel(book);
            return View(bookViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book model)
        {
            var isValidData = ModelState.IsValid;
            if (isValidData)
            {
                LibraryRepository.CreateBook(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book book = LibraryRepository.GetBookByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book model)
        {
            var isValidData = ModelState.IsValid;
            if (isValidData)
            {
                LibraryRepository.UpdateBook(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book bookModel = LibraryRepository.GetBookByID(id);
            if (bookModel == null)
            {
                return HttpNotFound();
            }
            return View(bookModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Book bookModel = LibraryRepository.GetBookByID(id);
            BookViewModel book = new BookViewModel(bookModel);

            if (book.Borrowers.Count > 0)
            {
                ViewBag.Error = "Delete fail: this book is borrowed. Please make sure the borrowers have returned all copies.";
                return View(bookModel);
            }

            LibraryRepository.DeleteBook(id);
            //TODO: throw an error if it cannot return 1 
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            LibraryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}