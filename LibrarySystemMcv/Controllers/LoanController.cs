using LibrarySystemMcv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LibrarySystemMcv.Controllers
{
    public class LoanController : BaseController {
        public ActionResult Index() {
            return View(Context.Loans);
        }

        public ActionResult Create() {
            return View();
        }

        public ActionResult Edit(int id) {
            return View(Context.Loans.Find(id));
        }

        public ActionResult Details(int id) {
            return View(Context.Loans.Find(id));
        }

        public async Task<ActionResult> Delete(int id) {
            Context.Loans.Remove(Context.Loans.Find(id));
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLoan(Loan model) {
            if (!ModelState.IsValid || Context.Books.FirstOrDefault(m => m.Id == model.BookId) == null || Context.Readers.FirstOrDefault(m => m.Id == model.ReaderId) == null) {
                TempData["Message"] = "Было передано неправильное Id объектов";
                TempData["MessageType"] = "danger";
                return RedirectToAction(nameof(Create));
            }
            Context.Loans.Add(model);
            TempData["Message"] = null;
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLoan(Loan model) {
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