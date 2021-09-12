using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _335as2.Dtos {
    public class StaffInputDto {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Url { get; set; }
        public string Research { get; set; }
    }
}
