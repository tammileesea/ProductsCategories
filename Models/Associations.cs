using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prodCat.Models {

    public class Association {
        [Key]
        public int AssociationId {get;set;}

        [Required]
        public int ItemID {get;set;}
        public Product Item {get;set;}
        
        [Required]
        public int GroupID {get;set;}
        public Category Group {get;set;}
    }
}