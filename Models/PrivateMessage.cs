using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_Snackis.Models
{
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string Receiver { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string SenderNickname { get; set; }
    }
}
