using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Forum_Snackis.Data;
using Forum_Snackis.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum_Snackis.Pages.Admin
{
    public class UpdateModel : PageModel
    {
        private readonly Forum_SnackisContext _snackisContext;

        [BindProperty]
        public string Header { get; set; }
        [BindProperty]
        public string Subject { get; set; }
        [BindProperty]
        public string Description { get; set; }
        public List<AdminFunctions> Functions { get; set; }
        [BindProperty(SupportsGet = true)]
        public int UpdateId { get; set; }

        public UpdateModel(Forum_SnackisContext snackisContext)
        {
            _snackisContext = snackisContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Functions = await _snackisContext.AdminFunctions.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UpdateId != 0)
            {
                AdminFunctions adminFunctions = await _snackisContext.AdminFunctions.FindAsync(UpdateId);
                adminFunctions.ForumHeader = Header;
                adminFunctions.Subject = Subject;
                adminFunctions.Description = Description;
                _snackisContext.SaveChanges();
            }

            return RedirectToPage("/Admin/Index");
        }
    }
}
