using LibraryProject.DataAccess;
using LibraryProject.Models;
using LibraryProject.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Hungarian Notation

namespace LibraryProject.Controllers
{
    //[RoutePrefix("Manager")]
    public class HomeController : Controller
    {
        private ILibraryRepository LibraryRepository { get; set; }

        public HomeController()
        {
            this.LibraryRepository = new LibraryRepository();
        }

        //[Route("Index")]
        public ActionResult Index()
        {
            List<BookModel> books = LibraryRepository.GetBooks();
            /*
            using (var context = new LibraryContext())
            {
                var newBook = new BookModel() { ISBN = "A", BookCopies = 10, Title="Test" };

                context.Books.Add(newBook);
                var result = context.SaveChanges();

                var bookList = context.Books;
                var db = context.Database;
                var c = db.Connection;
                var totalBooks = bookList.Count();
            }*/
            return View(books);
        }
        //[Route("Details")]
        public ActionResult Details (int ? id)
        {
            BookModel book = LibraryRepository.GetBookByID(id);
            return View(book);
        }
        //[Route("Create")]
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
        //[Route("Edit")]
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
        //[Route("Delete")]
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
    }
}