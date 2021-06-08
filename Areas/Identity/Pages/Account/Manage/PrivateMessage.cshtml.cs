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
using Microsoft.EntityFrameworkCore;

namespace Forum_Snackis.Areas.Identity.Pages.Account.Manage
{
    public class PrivateMessageModel : PageModel
    {
        private readonly Forum_SnackisContext _forum_SnackisContext;
        private readonly UserManager<Forum_SnackisUser> _userManager;

        [BindProperty(SupportsGet = true)]
        public string Receiver { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string Text { get; set; }
        public Forum_SnackisUser MyUser { get; set; }

        public List<PrivateMessage> PrivateMessages { get; set; }
        public List<Forum_SnackisUser> Users { get; set; }
        public List<PrivateMessage> Distinct { get; set; }

        public PrivateMessageModel(Forum_SnackisContext forum_SnackisContext, UserManager<Forum_SnackisUser> userManager)
        {
            _forum_SnackisContext = forum_SnackisContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            PrivateMessages = await _forum_SnackisContext.PrivateMessages.ToListAsync();
            Users = await _forum_SnackisContext.Users.ToListAsync();
            Distinct = PrivateMessages.GroupBy(g => g.Receiver).Select(g => g.First()).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            PrivateMessage privateMessage = new PrivateMessage();
            privateMessage.Receiver = Receiver;
            privateMessage.UserId = UserId;
            privateMessage.Text = Text;
            privateMessage.Date = DateTime.Now;

            _forum_SnackisContext.PrivateMessages.Add(privateMessage);
            await _forum_SnackisContext.SaveChangesAsync();

            return RedirectToPage("./PrivateMessage");
        }
    }
}
