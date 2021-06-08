using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_Snackis.Models
{
    public class Thread
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string WriterId { get; set; }
    }
}
