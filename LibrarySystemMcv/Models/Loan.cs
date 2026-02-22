using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibrarySystemMcv.Models {
    public class Loan {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name="ID книги")]
        public int BookId { get; set; }
        [Display(Name="ID читателя")]
        public int ReaderId { get; set; }
        [Required]
        [Display(Name = "Дата заимствования")]
        public DateTime BorrowDate { get; set; }
        [Display(Name = "Дата возврата")]
        public DateTime? ReturnDate { get; set; }
        [JsonIgnore]
        public virtual Book Book { get; set; }
        [JsonIgnore]
        public virtual Reader Reader { get; set; }
    }
}