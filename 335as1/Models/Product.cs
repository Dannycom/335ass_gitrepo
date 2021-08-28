using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _335as1.Models {
    public class Product {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Desciption { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
