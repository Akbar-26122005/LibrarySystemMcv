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

        public async Task<ActionResult> Delete(int id) {
            Context.Readers.Remove(Context.Readers.Find(id));
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateReader(Reader model) {
            if (ModelState.IsValid) {
                Context.Readers.Add(model);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReader(Reader model) {
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