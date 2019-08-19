using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prodCat.Models {
    public class Product {
        [Key]
        public int ProductId {get;set;}

        [Required]
        [MinLength(2, ErrorMessage = "Product name must be at least 2 characters")]
        [Display(Name = "Name")]
        public string Name {get;set;}

        [Required]
        [MinLength(2, ErrorMessage = "Product description must be at least 2 characters")]
        [Display(Name = "Description")]
        public string Description {get;set;}

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a minimum of 0 dollars")]
        [Display(Name = "Price")]
        public decimal Price {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Association> Groups {get;set;}
    }
}