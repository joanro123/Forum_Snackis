using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum_Snackis.Areas.Identity.Data;
using Forum_Snackis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum_Snackis.Pages
{
    public class InfoSiteModel : PageModel
    {
        private readonly Forum_SnackisContext _snackisContext;
        private readonly UserManager<Forum_SnackisUser> _userManager;
     

        public Forum_SnackisUser MyUser { get; set; }
        public List<Forum_SnackisUser> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public string UserNickname { get; set; }

        public InfoSiteModel(UserManager<Forum_SnackisUser> userManager, Forum_SnackisContext snackisContext)
        {
            _userManager = userManager;
            _snackisContext = snackisContext;

        }

        public async Task OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            Users = _snackisContext.Users.ToList();
        }
    }
}
