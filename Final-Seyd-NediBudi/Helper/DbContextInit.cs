using Final_Seyd_NediBudi.Enums;
using Final_Seyd_NediBudi.Models;
using Final_Seyd_NediBudi.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace Final_Seyd_NediBudi.Helper
{
    public class DbContextInit
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AdminVM _admin;

        public DbContextInit(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _admin = _configuration.GetSection("AdminSettings").Get<AdminVM>() ?? new();
        }
        public async Task Initalizer()
        {
            await CreateRole();
            await CreateAdmin();
        }
        private async Task CreateAdmin()
        {
            AppUser user = new()
            {
                UserName=_admin.UserName,
                Email=_admin.Email
            };
            var result = await _userManager.CreateAsync(user, _admin.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleEnum.Admin.ToString());
            }

        }
        private async Task CreateRole()
        {
            foreach(var role in Enum.GetNames(typeof(RoleEnum)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = role
                });
            }
        }
    }
}
