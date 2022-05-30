using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppRepositoryWithUOW.EF.Data;
using WebAppRepositoryWithUOW.EF.IdentityModels;

namespace WebAppRepositoryWithUOW.EF.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public DbInitializer(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            AppDbContext context)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            //apply migrations if they are not applied
            try
            {
                _context.Database.EnsureCreated();
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())   //add roles
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Student")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Instructor")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("HR")).GetAwaiter().GetResult();

                //if roles are not created, then we will create admin user as well
                var user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "01022330555",
                };

                _userManager.CreateAsync(user, "Admin123@").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
            return;
        }
    }
}
