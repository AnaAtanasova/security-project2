using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_backend.Dtos
{
    public class ChapChallenge
    {
        public int id { get; set; }
        public char answer { get; set; }
        public int index { get; set; }
        public string token { get; set; }
    }
}
