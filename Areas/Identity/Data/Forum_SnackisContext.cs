using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum_Snackis.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Forum_Snackis.Models;

namespace Forum_Snackis.Data
{
    public class Forum_SnackisContext : IdentityDbContext<Forum_SnackisUser>
    {
        public Forum_SnackisContext(DbContextOptions<Forum_SnackisContext> options)
            : base(options)
        {
        }
        public DbSet<AdminFunctions> AdminFunctions { get; set; }
        public DbSet<Insert> Inserts { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
