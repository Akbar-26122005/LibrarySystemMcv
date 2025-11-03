using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemMcv.Models {
    public class LoanViewModel {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> Readers { get; set; }

    }
}