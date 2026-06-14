using DomainLayer;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.DataSeed
{
    public class DataSeeding : IDataSeeding
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityDbContext _identityDbContext;

        public DataSeeding(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IdentityDbContext identityDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityDbContext = identityDbContext;
        }
        public async Task IdentityDataSeeding()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var use01 = new ApplicationUser()
                    {
                        Email = "khadad819@gmail.com",
                        DisPlayName = "Khaled Hadad",
                        UserName = "KhaledHadad",
                        PhoneNumber = "01152916595"
                    };
                     
                    var use02 = new ApplicationUser()
                    {
                        Email = "khadadMohamed819@gmail.com",
                        DisPlayName = "Khaled Mohamed",
                        UserName = "KhaledMohamed",
                        PhoneNumber = "01025643378"
                    };
                    await _userManager.CreateAsync(use01, "P@ssw0rd");
                    await _userManager.CreateAsync(use02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(use01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(use02, "Admin");
                }
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
