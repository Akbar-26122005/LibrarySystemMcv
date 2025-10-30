using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LibrarySystemMcv.Models {
    public class LibraryContext : DbContext
    {
        public static LibraryContext Instance = new LibraryContext();
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public LibraryContext() : base("DBConnection") { }
    }
}