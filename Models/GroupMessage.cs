using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_Snackis.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Member { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool AcceptInvitation { get; set; }
        public string Creator { get; set; }
    }
}
