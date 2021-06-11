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
    public class InsertModel : PageModel
    {
        private readonly Forum_SnackisContext _snackisContext;
        private readonly UserManager<Forum_SnackisUser> _userManager;

        [BindProperty(SupportsGet = true)]
        public string Subject { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Thread { get; set; }
        [BindProperty]
        public string Text { get; set; }
        public List<Insert> Inserts { get; set; }
        [BindProperty]
        public string Writer { get; set; }
        [BindProperty]
        public string WriterNickName { get; set; }
        public Forum_SnackisUser MyUser { get; set; }

        public InsertModel(Forum_SnackisContext snackisContext, UserManager<Forum_SnackisUser> userManager)
        {
            _snackisContext = snackisContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Insert insert = new Insert();
            insert.Date = DateTime.Now;
            insert.Subject = Subject;
            insert.Thread = Thread;
            insert.Text = Text;
            insert.Writer = Writer;
            insert.WriterNickName = WriterNickName;
            _snackisContext.Inserts.Add(insert);
           await _snackisContext.SaveChangesAsync();

            return RedirectToPage("/Threads", new {Subject = insert.Subject, Thread = insert.Thread });
        }
    }
}
