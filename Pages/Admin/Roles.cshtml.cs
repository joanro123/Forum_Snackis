using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Forum_Snackis.Areas.Identity.Data;

namespace Forum_Snackis.Pages.Admin
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManager<Forum_SnackisUser> _userManager;

        [BindProperty]
        public string Rolename { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }
        public bool IsUser { get; set; }
        public bool IsAdmin { get; set; }
        public Forum_SnackisUser CurrentUser { get; set; }

        public List<IdentityRole> RolesList { get; set; }

        public List<Forum_SnackisUser> Users { get; set; }

        public RolesModel(RoleManager<IdentityRole> roleManager, UserManager<Forum_SnackisUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            RolesList = _roleManager.Roles.ToList();
            Users = _userManager.Users.ToList();

            if (AddUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                var roleResult = await _userManager.AddToRoleAsync(alterUser, Role);
            }

            if (RemoveUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                var roleResult = await _userManager.RemoveFromRoleAsync(alterUser, Role);
            }

            CurrentUser = await _userManager.GetUserAsync(User);
            IsUser = await _userManager.IsInRoleAsync(CurrentUser, "User");
            IsAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Rolename != null)
            {
                await CreateRole(Rolename);
            }

            return RedirectToPage("./Roles");
        }

        public async Task CreateRole(string roleName)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleName);

            if (!exist)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };

                await _roleManager.CreateAsync(role);
            }

            
        }
    }
}
