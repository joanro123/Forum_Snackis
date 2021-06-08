using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Forum_Snackis.Data;
using Forum_Snackis.Models;
using Forum_Snackis.Areas.Identity.Data;

namespace Forum_Snackis.Pages.Admin
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Header { get; set; }
        [BindProperty]
        public string Subject { get; set; }
        [BindProperty]
        public string Description { get; set; }
        public List<AdminFunctions> Functions { get; set; }

        private readonly Forum_SnackisContext _forum_SnackisContext;
        [BindProperty(SupportsGet = true)]
        public int DeleteId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int UpdateId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OffensiveId { get; set; }
        public List<AdminFunctions> Distinct { get; set; }
        public List<Thread> Threads { get; set; }
        public List<Insert> Inserts { get; set; }
        public List<Forum_SnackisUser> Users { get; set; }

        public IndexModel(Forum_SnackisContext forum_SnackisContext)
        {
            _forum_SnackisContext = forum_SnackisContext;
        }

        public IActionResult OnGet()
        {
            Functions = _forum_SnackisContext.AdminFunctions.ToList();
            Threads = _forum_SnackisContext.Threads.ToList();
            Inserts = _forum_SnackisContext.Inserts.ToList();
            Users = _forum_SnackisContext.Users.ToList();

            Distinct = Functions.GroupBy(g => g.ForumHeader).Select(s => s.First()).ToList();

            if (UpdateId != 0)
            {
                AdminFunctions adminFunctions = _forum_SnackisContext.AdminFunctions.Find(UpdateId);

                if (adminFunctions != null)
                {
                    return RedirectToPage("/Admin/Update", new { UpdateId });
                }
            }

            if (DeleteId != 0)
            {
                AdminFunctions adminFunctions = _forum_SnackisContext.AdminFunctions.Find(DeleteId);

                if (adminFunctions != null)
                {
                    _forum_SnackisContext.Remove(adminFunctions);
                    
                    foreach (var item in Threads.Where(w => w.Subject == adminFunctions.Subject))
                    {
                        _forum_SnackisContext.Remove(item);
                    }
                    foreach (var item in Inserts.Where(w => w.Subject == adminFunctions.Subject))
                    {
                        _forum_SnackisContext.Remove(item);
                    }
                    _forum_SnackisContext.SaveChanges();
                    return RedirectToPage("/Admin/Index");
                }

            }

            if (OffensiveId != 0)
            {
                Insert insert = _forum_SnackisContext.Inserts.Find(OffensiveId);

                if (insert != null)
                {
                    _forum_SnackisContext.Remove(insert);
                    _forum_SnackisContext.SaveChanges();
                    return RedirectToPage("/Admin/Index");
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            AdminFunctions adminFunctions = new AdminFunctions();
            adminFunctions.ForumHeader = Header;
            adminFunctions.Subject = Subject;
            adminFunctions.Description = Description;
            _forum_SnackisContext.AdminFunctions.Add(adminFunctions);
            _forum_SnackisContext.SaveChanges();


            return RedirectToPage("/Admin/Index");
        }
    }
}
