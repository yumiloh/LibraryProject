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
    public class ManagerController : LibraryBaseController
    {
        private IManagerRepository BorrowerRepository;

        public ManagerController(IManagerRepository borrowerRepository)
        {
            this.BorrowerRepository = borrowerRepository;
        }

        public ActionResult Index()
        {
            if (!this.IsManagerLoggedIn)
            {
                return RedirectToAction("Index", "Authentication");
            }

            List<BorrowerModel> borrowerModel = BorrowerRepository.GetBorrower();
            return View(borrowerModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var borrowerModel = new BorrowerViewModel(BorrowerRepository.GetBorrowerByID(id));
            if (borrowerModel == null)
            {
                return HttpNotFound();
            }
            return View(borrowerModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BorrowerModel borrowerModel)
        {
            if (ModelState.IsValid)
            {
                BorrowerRepository.CreateBorrower(borrowerModel);
                return RedirectToAction("Index");
            }
            return View(borrowerModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowerModel borrowerModel = BorrowerRepository.GetBorrowerByID(id);
            if (borrowerModel == null)
            {
                return HttpNotFound();
            }
            return View(borrowerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Name")] BorrowerModel borrowerModel)
        {
            if (ModelState.IsValid)
            {
                BorrowerRepository.UpdateBorrower(borrowerModel);
                return RedirectToAction("Index");
            }
            return View(borrowerModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowerModel borrowerModel = BorrowerRepository.GetBorrowerByID(id);
            if (borrowerModel == null)
            {
                return HttpNotFound();
            }
            return View(borrowerModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowerRepository.DeleteBorrower(id);
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            BorrowerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
