using LibrarySystemMcv.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemMcv.Controllers
{
    public class BookController : BaseController {
        private static ViewData<Book> _viewData;

        public BookController(): base() {
            _viewData = new ViewData<Book>();
            _viewData.TryInit(() => Context.Books.ToList());
            _viewData.Update();
        }

        public ActionResult Index() {
            _viewData.Update();
            return View(_viewData);
        }

        public ActionResult Create() {
            return View();
        }

        public ActionResult Edit(int id) {
            return View(Context.Books.Find(id));
        }

        public ActionResult Details(int id) {
            return View(Context.Books.Find(id));
        }

        public ActionResult Delete(int id) {
            Context.Books.Remove(Context.Books.Find(id));
            Context.SaveChanges();
            TempData["Success"] = "Данные читателя были успешно удалены";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(Book model) {
            if (ModelState.IsValid) {
                Context.Books.Add(model);
                Context.SaveChanges();
                TempData["Success"] = "Данные читателя были успешно добавлены";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(Book model) {
            if (ModelState.IsValid) {
                try {
                    Context.Entry(model).State = EntityState.Modified;
                    Context.SaveChanges();
                    TempData["Success"] = "Данные читателя были успешно изменены";
                    return RedirectToAction(nameof(Index));
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyFilters(ViewData<Book> data) {
            _viewData.SearchSubstring = data.SearchSubstring;
            _viewData.ApplyChanges();
            return View(nameof(Index), _viewData);
        }

        [HttpGet]
        public ActionResult DropFilters() {
            _viewData.ClearFilters();
            return RedirectToAction(nameof(Index));
        }
    }
}