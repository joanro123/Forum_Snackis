using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Forum_Snackis.Data;
using Forum_Snackis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Forum_Snackis.Areas.Identity.Data;
using System.Text.RegularExpressions;

namespace Forum_Snackis.Pages
{
    public class ThreadsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Thread { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Subject { get; set; }
       
        [BindProperty]
        public int OffensiveId { get; set; }
        [BindProperty]
        public string PhotoLink { get; set; }
        [BindProperty(SupportsGet = true)]
        public int InsertId { get; set; }

        public List<Insert> Inserts { get; set; }
        public List<Forum_SnackisUser> Users { get; set; }

        private readonly Forum_SnackisContext _snackisContext;
        private readonly UserManager<Forum_SnackisUser> _userManager;
        private readonly SignInManager<Forum_SnackisUser> _signInManager;

        public Forum_SnackisUser MyUser { get; set; }

        public ThreadsModel(Forum_SnackisContext snackisContext, UserManager<Forum_SnackisUser> userManager, SignInManager<Forum_SnackisUser> signInManager)
        {
            _snackisContext = snackisContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            Inserts = await _snackisContext.Inserts.ToListAsync();
            MyUser = await _userManager.GetUserAsync(User);
            Users = await _snackisContext.Users.ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Insert insert = new Insert();

            if (OffensiveId != 0)
            {
                insert = await _snackisContext.Inserts.FindAsync(OffensiveId);

                if (insert != null)
                {
                    insert.Offensive = true;
                    await _snackisContext.SaveChangesAsync();
                    
                }

            }

            if (InsertId != 0)
            {

                insert = await _snackisContext.Inserts.FindAsync(InsertId);

                if (insert != null)
                {
                    insert.PhotoLink = PhotoLink;
                    await _snackisContext.SaveChangesAsync();
                }
            }

            return RedirectToPage("/Threads", new { Subject = insert.Subject, Thread = insert.Thread });
        }

        public async Task<IActionResult> OnPostAddLike(int id)
        {
            Insert insert = await _snackisContext.Inserts.FindAsync(id);

            if (insert != null)
            {
                insert.Like++;
                await _snackisContext.SaveChangesAsync();
            }

            return RedirectToPage("/Threads", new { Subject = insert.Subject, Thread = insert.Thread });
        }
        public async Task<IActionResult> OnPostAddHearts(int id)
        {
            Insert insert = await _snackisContext.Inserts.FindAsync(id);

            if (insert != null)
            {
                insert.Hearts++;
                await _snackisContext.SaveChangesAsync();
            }

            return RedirectToPage("/Threads", new { Subject = insert.Subject, Thread = insert.Thread });
        }
        public async Task<IActionResult> OnPostAddDislike(int id)
        {
            Insert insert = await _snackisContext.Inserts.FindAsync(id);

            if (insert != null)
            {
                insert.Dislike++;
                await _snackisContext.SaveChangesAsync();
            }

            return RedirectToPage("/Threads", new {Subject = insert.Subject, Thread = insert.Thread });
        } 

        public string BadFilter(string text)
        {

            Regex filter = new Regex("(bil|kiss|bajskorv|avf?ring)", RegexOptions.IgnoreCase);
            return filter.Replace(text, "****");
        }
    }
}
