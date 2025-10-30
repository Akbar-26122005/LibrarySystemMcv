using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibrarySystemMcv.Models {
    public class Book {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        [Range(0, 3000)]
        public int Year { get; set; }
        [Range(0, 1000)]
        public int Quantity { get; set; }
        public bool Available { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}