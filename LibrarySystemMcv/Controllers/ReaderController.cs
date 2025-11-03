using LibrarySystemMcv.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemMcv.Controllers
{
    public class ReaderController : BaseController {
        public ActionResult Index() {
            return View(Context.Readers);
        }

        public ActionResult Create() {
            return View();
        }

        public ActionResult Edit(int id) {
            return View(Context.Readers.Find(id));
        }

        public ActionResult Details(int id) {
            return View(Context.Readers.Find(id));
        }

        public ActionResult Delete(int id) {
            Context.Readers.Remove(Context.Readers.Find(id));
            Context.SaveChanges();
            TempData["Success"] = "Данные читателя успешно удалены";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReader(Reader model) {
            if (ModelState.IsValid) {
                Context.Readers.Add(model);
                Context.SaveChanges();
                TempData["Success"] = "Давнные читателя успешно добавлены";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReader(Reader model) {
            if (ModelState.IsValid) {
                try {
                    Context.Entry(model).State = EntityState.Modified;
                    Context.SaveChanges();
                    TempData["Success"] = "Данные читателя успешно изменены";
                    return RedirectToAction(nameof(Index));
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }

            return View(model);
        }
    }
}