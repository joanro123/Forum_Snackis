using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Forum_Snackis.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum_Snackis.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Forum_SnackisUser> _userManager;
        private readonly SignInManager<Forum_SnackisUser> _signInManager;

        public IndexModel(
            UserManager<Forum_SnackisUser> userManager,
            SignInManager<Forum_SnackisUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Användarnamn")]
            public string NickName { get; set; }

            [Display(Name = "Bildlänk")]
            public string Photo { get; set; }
            [Display(Name = "Hemstad")]
            public string Hometown { get; set; }
            [Display(Name = "Om dig")]
            public string Description { get; set; }
        }

        private async Task LoadAsync(Forum_SnackisUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                NickName = user.NickName,
                Photo = user.Photo,
                Hometown = user.Hometown,
                Description = user.Description
                
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.NickName != user.NickName)
            {
                user.NickName = Input.NickName;
            }
            if (Input.Photo != user.Photo)
            {
                user.Photo = Input.Photo;
            }
            if (Input.Hometown != user.Hometown)
            {
                user.Hometown = Input.Hometown;
            }
            if (Input.Description != user.Description)
            {
                user.Description = Input.Description;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
