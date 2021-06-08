using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Forum_Snackis.Areas.Identity.Data;
using Forum_Snackis.Data;
using Microsoft.AspNetCore.Identity;
using Forum_Snackis.Models;

namespace Forum_Snackis.Areas.Identity.Pages.Account.Manage
{
    public class GroupMessageModel : PageModel
    {
       
        private readonly Forum_SnackisContext _SnackisContext;
        private readonly UserManager<Forum_SnackisUser> _userManager;
        public List<Forum_SnackisUser> Users { get; set; }
        public Forum_SnackisUser MyUser { get; set; }
        [BindProperty]
        public string Receiver { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string Text { get; set; }
        public List<PrivateMessage> PrivateMessages { get; set; }
        public List<PrivateMessage> Distinct { get; set; }


        public GroupMessageModel(Forum_SnackisContext snackisContext, UserManager<Forum_SnackisUser> userManager)
        {
        
            _SnackisContext = snackisContext;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            Users = _SnackisContext.Users.ToList();
            MyUser = await _userManager.GetUserAsync(User);
            PrivateMessages = _SnackisContext.PrivateMessages.ToList();
            Distinct = PrivateMessages.GroupBy(g => g.Receiver).Select(g => g.First()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            PrivateMessage privateMessage = new PrivateMessage();
            privateMessage.Receiver = Receiver;
            privateMessage.UserId = UserId;
            privateMessage.Text = Text;
            privateMessage.Date = DateTime.Now;

            _SnackisContext.PrivateMessages.Add(privateMessage);              
            
            await _SnackisContext.SaveChangesAsync();
         

            return RedirectToPage("./GroupMessage");
        }
    }
}
