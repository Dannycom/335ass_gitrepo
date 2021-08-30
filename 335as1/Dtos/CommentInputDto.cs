using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _335as1.Dtos {
    public class CommentInputDto {
       
        public DateTime Time { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}
