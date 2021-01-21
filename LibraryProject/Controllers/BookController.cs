using LibraryProject.Enums;
using LibraryProject.Models;
using LibraryProject.Repository;
using LibraryProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class BookController : Controller
    {
        private ILibraryRepository LibraryRepository { get; set; }

        public BookController(ILibraryRepository libraryRepository)
        {
            this.LibraryRepository = libraryRepository;
        }

        public ActionResult Index()
        {
            List<BookModel> books = LibraryRepository.GetBooks();
            ViewBag.IsManager = (Convert.ToString(Session["UserRole"]).Equals(UserRole.Manager.ToString()));
            return View(books);
        }
        
        public ActionResult Details (int ? id)
        {
            BookModel book = LibraryRepository.GetBookByID(id);
            var bookViewModel = new BookViewModel(book);
            return View(bookViewModel);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookModel model)
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
            BookModel book = LibraryRepository.GetBookByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(BookModel model)
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
            BookModel book = LibraryRepository.GetBookByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
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