using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum_Snackis.Data;
using Forum_Snackis.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum_Snackis.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Forum_SnackisContext _snackisContext;
        public List<AdminFunctions> AdminFunctions { get; set; }
        public List<Thread> Threads { get; set; }
        public List<Insert> Inserts { get; set; }

      //  private readonly IInsertGateway _gateway;
     //   public List<Insert> GatewayInserts { get; set; }

        public IndexModel(Forum_SnackisContext snackisContext)
        {
            _snackisContext = snackisContext;
         //   _gateway = gateway;
        }

        public async Task OnGetAsync()
        {
            AdminFunctions = await _snackisContext.AdminFunctions.ToListAsync();
            Threads = await _snackisContext.Threads.ToListAsync();
            Inserts = await _snackisContext.Inserts.ToListAsync();
        //    GatewayInserts = await _gateway.GetInserts();
        }

        public int CountThreads(string subject)
        {
            int x = Threads.Where(w => w.Subject == subject).Select(s => s.Name).Count();
            return x;   
        }
        public int CountInserts(string subject)
        {
            int x = Inserts.Where(w => w.Subject == subject).Count();
            return x;
        }

    }
}
