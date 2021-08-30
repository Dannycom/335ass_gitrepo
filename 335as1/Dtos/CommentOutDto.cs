using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _335as1.Dtos {
    public class CommentOutDto {

        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}
