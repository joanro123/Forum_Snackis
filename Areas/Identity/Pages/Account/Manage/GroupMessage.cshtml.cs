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
       
        private readonly Forum_SnackisContext _snackisContext;
        private readonly UserManager<Forum_SnackisUser> _userManager;
        public List<Forum_SnackisUser> Users { get; set; }
        public Forum_SnackisUser MyUser { get; set; }
        [BindProperty]
        public string GroupName { get; set; }
        
        [BindProperty]
        public string Creator { get; set; }
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public string Member { get; set; }
        [BindProperty]
        public bool Accept { get; set; }
        [BindProperty(SupportsGet = true)]
        public int DeleteUserId { get; set; }

        public List<GroupMessage> GroupMessages { get; set; }
        public List<GroupMessage> Distinct { get; set; }
        public List<GroupMessage> GroupsWithMyUser { get; set; }


        public GroupMessageModel(Forum_SnackisContext snackisContext, UserManager<Forum_SnackisUser> userManager)
        {
        
            _snackisContext = snackisContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Users = _snackisContext.Users.ToList();
            MyUser = await _userManager.GetUserAsync(User);
            GroupMessages = _snackisContext.GroupMessages.ToList();
            Distinct = GroupMessages.GroupBy(g => new { g.Name, g.Member, g.Creator }).Select(s => s.First()).ToList();
            GroupsWithMyUser = GroupMessages.Where(w => w.Member == MyUser.NickName || w.Creator == MyUser.NickName).ToList();
            

            if (DeleteUserId != 0)
            {
                GroupMessage groupMessage = await _snackisContext.GroupMessages.FindAsync(DeleteUserId);

                if (groupMessage != null)
                {
                    _snackisContext.Remove(groupMessage);
                    await _snackisContext.SaveChangesAsync();
                    return RedirectToPage("./Groupmessage");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            GroupMessage groupMessage = new GroupMessage();
            groupMessage.Name = GroupName;
            groupMessage.Creator = Creator;
            groupMessage.Member = Member;
            groupMessage.AcceptInvitation = Accept;
            groupMessage.Message = Text;
            groupMessage.Date = DateTime.Now;

            _snackisContext.GroupMessages.Add(groupMessage);
            await _snackisContext.SaveChangesAsync();
         

            return RedirectToPage("./GroupMessage");
        }

        public async Task<IActionResult> OnPostAcceptInvitation(int id)
        {
            GroupMessage groupMessage = await _snackisContext.GroupMessages.FindAsync(id);

            if (groupMessage != null)
            {
                groupMessage.AcceptInvitation = true;
                await _snackisContext.SaveChangesAsync();
            }

            return RedirectToPage("./GroupMessage");
        }
    }
}
