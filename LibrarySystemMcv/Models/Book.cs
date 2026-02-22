using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibrarySystemMcv.Models {
    public class Book {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Range(0, 3000)]
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }
        [Range(0, 1000)]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        [Display(Name = "Доступность")]
        public bool Available { get; set; }
        [JsonIgnore]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}