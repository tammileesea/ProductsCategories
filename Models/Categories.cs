using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prodCat.Models {
    public class Category {
        [Key]
        public int CategoryId {get;set;}

        [Required]
        [MinLength(2, ErrorMessage = "Category name must be at least 2 characters")]
        [Display(Name = "Name")]
        public string Name {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Association> Items {get;set;}
    }
}