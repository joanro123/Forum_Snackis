using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Forum_Snackis.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Forum_SnackisUser class
    public class Forum_SnackisUser : IdentityUser
    {
        [PersonalData]
        public string NickName { get; set; }

        [PersonalData]
        public string Photo { get; set; }
        [PersonalData]
        public string Hometown { get; set; }
        [PersonalData]
        public string Description { get; set; }
    }
}
