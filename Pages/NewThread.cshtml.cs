using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Forum_Snackis.Data;
using Forum_Snackis.Models;
using Microsoft.AspNetCore.Identity;
using Forum_Snackis.Areas.Identity.Data;

namespace Forum_Snackis.Pages
{
    public class NewThreadModel : PageModel
    {
        private readonly Forum_SnackisContext _snackisContext;
        private readonly UserManager<Forum_SnackisUser> _userManager;
        private readonly SignInManager<Forum_SnackisUser> _signInManager;

        [BindProperty(SupportsGet = true)]
        public string Subject { get; set; }

        [BindProperty]
        public string Name { get; set; }
        public List<Thread> Threads { get; set; }
        public Forum_SnackisUser MyUser { get; set; }
        [BindProperty]
        public string WriterId { get; set; }

        public NewThreadModel(Forum_SnackisContext snackisContext, UserManager<Forum_SnackisUser> userManager, SignInManager<Forum_SnackisUser> signInManager)
        {
            _snackisContext = snackisContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);

        }

        public async  Task<IActionResult> OnPostAsync()
        {
            Thread thread = new Thread();
            thread.Subject = Subject;
            thread.Name = Name;
            thread.WriterId = WriterId;
            _snackisContext.Threads.Add(thread);
            await _snackisContext.SaveChangesAsync();

            return RedirectToPage("/Forums", new { Subject = thread.Subject });
        }
    }
}
