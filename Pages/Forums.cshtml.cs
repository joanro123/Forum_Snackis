using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Forum_Snackis.Data;
using Forum_Snackis.Models;
using Forum_Snackis.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Forum_Snackis.Pages
{
    public class ForumsModel : PageModel
    {
        private readonly Forum_SnackisContext _snackisContext;

        private readonly UserManager<Forum_SnackisUser> _userManager;
        private readonly SignInManager<Forum_SnackisUser> _signInManager;

        [BindProperty(SupportsGet = true)]
        public string Subject { get; set; }
        public List<Thread> Threads { get; set; }
        public List<Insert> Inserts { get; set; }
        public List<Insert> Inserts2 { get; set; }
        public DateTime Latest { get; set; }
        public List<Forum_SnackisUser> Users { get; set; }
        public Forum_SnackisUser MyUser { get; set; }

        public ForumsModel(Forum_SnackisContext snackisContext, UserManager<Forum_SnackisUser> userManager, SignInManager<Forum_SnackisUser> signInManager)
        {
            _snackisContext = snackisContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            Threads = _snackisContext.Threads.ToList();
            Inserts = _snackisContext.Inserts.OrderByDescending(o => o.Date).Take(1).ToList();
            Users = _snackisContext.Users.ToList();
            Inserts2 = _snackisContext.Inserts.ToList();
            MyUser = await _userManager.GetUserAsync(User);
        }

        public int CountInserts(string thread)
        {
            int x = Inserts2.Count(c => c.Thread == thread);
            return x;
        }

        public DateTime LatestInsert(string thread)
        {
            DateTime date = Inserts2.Where(w => w.Thread == thread).Select(s => s.Date).Last();
            return date;
        }
    }
}
