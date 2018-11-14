using AspNetCore21MvcIdentity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCore21MvcIdentity.Data
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            // Seed products
            if (!_context.Products.Any())
            {
                _context.AddRange
                (
                    new Product
                    {
                        Title = "Product 1",
                        Price = 199.95M,
                        Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    },
                    new Product
                    {
                        Title = "Product 2",
                        Price = 299.95M,
                        Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    },
                    new Product
                    {
                        Title = "Product 3",
                        Price = 399.95M,
                        Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don\'t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn\'t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                    }
                );
            }

            // Seed users
            var user = await _userManager.FindByNameAsync("karrman");

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = "karrman",
                    FirstName = "Karr",
                    LastName = "Man",
                    Email = "karr@karrman.com"
                };

                if (!await _roleManager.RoleExistsAsync("admin"))
                {
                    var role = new IdentityRole("admin");
                    await _roleManager.CreateAsync(role);
                    await _roleManager.AddClaimAsync(role, new Claim("IsAdmin", "True"));
                }

                var userResult = await _userManager.CreateAsync(user, "p4$$W0rD!");
                var roleResult = await _userManager.AddToRoleAsync(user, "admin");
                var claimResult = await _userManager.AddClaimAsync(user, new Claim("IsSuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                    throw new InvalidOperationException("Failed to build user and roles!");
            }
        }
    }
}