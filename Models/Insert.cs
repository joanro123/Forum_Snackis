using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_Snackis.Models
{
    public class Insert
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Writer { get; set; }
        public string WriterNickName { get; set; }
        public string Subject { get; set; }
        public string Thread { get; set; }
        public string Text { get; set; }
        public bool Offensive { get; set; }
        public string PhotoLink { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Hearts { get; set; }
        public int ParentId { get; set; }
    }
}
