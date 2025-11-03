using LibrarySystemMcv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

namespace LibrarySystemMcv.Controllers
{
    public class LoanController : BaseController {
        public ActionResult Index() {
            return View(Context.Loans);
        }

        public void PopulateDropdowns(LoanViewModel model) {
            model.Books = Context.Books.Select(m => new SelectListItem {
                Value = m.Id.ToString(),
                Text = m.Title + " - " + m.Author
            }).ToList();

            model.Readers = Context.Readers.Select(m => new SelectListItem {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
        }

        public ActionResult Create() {
            var model = new LoanViewModel {
                BorrowDate = DateTime.Now,
                Books = Context.Books.Select(m => new SelectListItem {
                    Value = m.Id.ToString(),
                    Text = m.Title + " - " + m.Author
                }).ToList(),
                Readers = Context.Readers.Select(m => new SelectListItem {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Edit(int? id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Loan loan = Context.Loans.Find(id);
            if (loan == null) return HttpNotFound();

            var model = new LoanViewModel {
                LoanId = loan.Id,
                BookId = loan.BookId,
                ReaderId = loan.ReaderId,
                BorrowDate = loan.BorrowDate,
                ReturnDate = loan.ReturnDate
            };
            PopulateDropdowns(model);

            return View(model);
        }

        public ActionResult Details(int id) {
            return View(Context.Loans.Find(id));
        }

        public ActionResult Delete(int id) {
            Context.Loans.Remove(Context.Loans.Find(id));
            Context.SaveChanges();
            TempData["Success"] = "Данные займа были успешно удалены";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoanViewModel model) {
            if (ModelState.IsValid) {
                var loan = new Loan {
                    BookId = model.BookId,
                    ReaderId = model.ReaderId,
                    BorrowDate = model.BorrowDate,
                    ReturnDate = model.ReturnDate
                };

                Context.Loans.Add(loan);
                Context.SaveChanges();

                TempData["Success"] = "Данные займа были успешно добавлены";
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LoanViewModel model) {
            if (ModelState.IsValid) {
                var loan = Context.Loans.Find(id);
                if (loan == null) return HttpNotFound();

                loan.BookId = model.BookId;
                loan.ReaderId = model.ReaderId;
                loan.BorrowDate = model.BorrowDate;
                loan.ReturnDate = model.ReturnDate;

                Context.Entry(loan).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Success"] = "Данные займа были успешно изменены";
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(model);
            return View(model);
        }
    }
}