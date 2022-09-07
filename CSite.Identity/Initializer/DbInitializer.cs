using CSite.Identity.DbContexts;
using CSite.Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CSite.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Supplier)).GetAwaiter().GetResult();
            }
            else { return; }

            ApplicationUser adminUser = new ApplicationUser()
            {
                Id= "4cd6cba2-4950-42a2-bb48-14f09aef239d",
                UserName = "admin",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "(+1) 343 5672 322",
                FirstName = "Amin",
                LastName = "Norollah"
            };

            _userManager.CreateAsync(adminUser, "CSite@123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,adminUser.FirstName+" "+ adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName,adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Admin),
            }).Result;

            ApplicationUser customerUser = new ApplicationUser()
            {
                Id = "5cd6cba2-4950-42a2-bb48-14f09aef239d",
                UserName = "customer",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "(+1) 343 5672 322",
                FirstName = "Joe",
                LastName = "Morgan"
            };

            _userManager.CreateAsync(customerUser, "CSite@123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,customerUser.FirstName+" "+ customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName,customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,customerUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Customer),
            }).Result;

            ApplicationUser editorUser = new ApplicationUser()
            {
                Id = "6cd6cba2-4950-42a2-bb48-14f09aef239d",
                UserName = "supplier",
                Email = "supplier1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "(+1) 343 5672 322",
                FirstName = "Sara",
                LastName = "Jackson"
            };

            _userManager.CreateAsync(editorUser, "CSite@123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(editorUser, SD.Supplier).GetAwaiter().GetResult();

            var temp3 = _userManager.AddClaimsAsync(editorUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,editorUser.FirstName+" "+ editorUser.LastName),
                new Claim(JwtClaimTypes.GivenName,editorUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,editorUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Supplier),
            }).Result;
        }
    }
}
