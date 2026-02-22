using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibrarySystemMcv.Models {
    public class Reader {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name="Номер телефона")]
        public string Phone { get; set; }
        [JsonIgnore]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}