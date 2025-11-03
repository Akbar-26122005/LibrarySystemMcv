using LibrarySystemMcv.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LibrarySystemMcv.Controllers {
    public class HomeController : BaseController {
        public ActionResult Index() {
            var cutoffDate = DateTime.Now.AddDays(-30);

            var stats = new DashboardStats {
                TotalBooks = Context.Books.Count(),
                TotalReaders = Context.Readers.Count(),
                ActiveLoans = Context.Loans.Count(l => l.ReturnDate == null),
                OverdueLoans = Context.Loans
                    .Where(l => l.ReturnDate == null && l.BorrowDate < cutoffDate)
                    .Count()
            };

            var recentLoans = Context.Loans
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .OrderByDescending(l => l.BorrowDate)
                .Take(5)
                .ToList();

            var viewModel = new DashboardViewModel {
                Stats = stats,
                RecentLoans = recentLoans
            };

            return View(viewModel);
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Contact() {
            return View();
        }

        [HttpGet]
        public ActionResult DownloadJson() {
            var data = GetCombinedData();

            var filepath = Server.MapPath("/App_Data/data.json");
            var json = ExportToJsonFile(filepath);

            return File(
                System.Text.Encoding.UTF8.GetBytes(json),
                "application/json",
                "data.json"
            );
        }

        public object GetCombinedData() {
            return new {
                Books   = Context.Books.ToList(),
                Readers = Context.Readers.ToList(),
                Loans   = Context.Loans.ToList()
            };
        }

        public string ExportToJsonFile(string filePath) {
            var data = GetCombinedData();

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
            return json;
        }
    }

    public class DashboardStats {
        public int TotalBooks { get; set; }
        public int TotalReaders { get; set; }
        public int ActiveLoans { get; set; }
        public int OverdueLoans { get; set; }
    }

    public class DashboardViewModel {
        public DashboardStats Stats { get; set; }
        public List<Loan> RecentLoans { get; set; }
    }
}