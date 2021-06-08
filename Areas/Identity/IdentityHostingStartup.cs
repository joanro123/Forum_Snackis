using System;
using Forum_Snackis.Areas.Identity.Data;
using Forum_Snackis.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Forum_Snackis.Areas.Identity.IdentityHostingStartup))]
namespace Forum_Snackis.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Forum_SnackisContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ronnqvistsnackisdb2")));

                services.AddDefaultIdentity<Forum_SnackisUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Forum_SnackisContext>();
            });
        }
    }
}