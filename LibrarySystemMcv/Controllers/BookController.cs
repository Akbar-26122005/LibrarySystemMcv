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
        public ActionResult Index() {
            return View(Context.Books.ToList());
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

        public async Task<ActionResult> Delete(int id) {
            Context.Books.Remove(Context.Books.Find(id));
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBook(Book model) {
            if (ModelState.IsValid) {
                Context.Books.Add(model);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook(Book model) {
            if (ModelState.IsValid) {
                try {
                    Context.Entry(model).State = EntityState.Modified;
                    await Context.SaveChangesAsync();
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}