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
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }

        [JsonIgnore]
        public virtual Book Book { get; set; }
        [JsonIgnore]
        public virtual Reader Reader { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}