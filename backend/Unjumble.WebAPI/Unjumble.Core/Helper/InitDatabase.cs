using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unjumble.Core.Models;

namespace Unjumble.Core.Helper
{
    public static class InitDatabase
    {
        public static async Task Migrate(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            var roles = new List<string>()
            {
                "Admin",
                "User"
            };

            foreach (var role in roles)
            {
                var identityRole = new IdentityRole() { Name = role };
                await roleManager.CreateAsync(identityRole);
            }

            var email = "admin@unjubmle.io";
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new User()
                {
                    FullName = "Unjumble Admin Team",
                    UserName = "Administrator",
                    Email = email,
                    EmailConfirmed = true
                };

                var createdAdmin = await userManager.CreateAsync(admin, "Jakojakalozinka1");
                if (createdAdmin.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }


           /* var imena = new List<string>()
            {
               "John Andersson",
               "Mike Morrison",
               "Jess Mileston",
               "Sabit Tarčin",
               "Alex Washington",
               "Ben Larsson",
               "Clara Querry"
            };

            var urls = new List<string>()
            {
               "https://www.med-decisions.com/everything-you-need-to-know-about-the-male-sex-drive/",
               "https://images.pexels.com/photos/2379004/pexels-photo-2379004.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
               "https://stylesatlife.com/wp-content/uploads/2018/04/Chic-Long-Hairstyle-for-Everyday.jpg",
               "https://pbs.twimg.com/profile_images/884370819278876672/O86le8Oq.jpg",
               "https://images.unsplash.com/photo-1529626455594-4ff0802cfb7e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1000&q=80",
               "https://post.medicalnewstoday.com/wp-content/uploads/sites/3/2020/02/266749_2200-732x549.jpg",
               "https://1.bp.blogspot.com/-lb7JymdHb8A/XYC2dkKgxYI/AAAAAAAAAUE/zr3Duicbnh4mxk-idN37hlVacigySMyUQCLcBGAsYHQ/s640/Girl%2BImage%2BDownload%2B%25283%2529.jpg"
            };

            for (int i = 0; i < 7; ++i)
            {
                email = "user" + i + "@unjumble.io";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var admin = new User()
                    {
                        FullName = imena[i],
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true,
                        PhotoUrl = urls[i]
                    };

                    var createdAdmin = await userManager.CreateAsync(admin, "amaramar1");
                    if (createdAdmin.Succeeded)
                        await userManager.AddToRoleAsync(admin, "User");
                }
            }*/
        }
    }
}
