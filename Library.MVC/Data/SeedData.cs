using Bogus;
using Library.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.MVC.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Books.Any() || context.Members.Any())
            {
                return;
            }

            // Books (20)
            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Commerce.ProductName())
                .RuleFor(b => b.Author, f => f.Person.FullName)
                .RuleFor(b => b.Isbn, f => f.Random.Replace("###-##########"))
                .RuleFor(b => b.Category, f => f.Commerce.Categories(1)[0])
                .RuleFor(b => b.IsAvailable, true);

            var books = bookFaker.Generate(20);
            context.Books.AddRange(books);
            context.SaveChanges();

            // Members (10)
            var memberFaker = new Faker<Member>()
                .RuleFor(m => m.FullName, f => f.Person.FullName)
                .RuleFor(m => m.Email, f => f.Internet.Email())
                .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber());

            var members = memberFaker.Generate(10);
            context.Members.AddRange(members);
            context.SaveChanges();

            // Loans (15)
            var loanFaker = new Faker<Loan>()
                .RuleFor(l => l.BookId, f => f.PickRandom(books).Id)
                .RuleFor(l => l.MemberId, f => f.PickRandom(members).Id)
                .RuleFor(l => l.LoanDate, f => DateOnly.FromDateTime(f.Date.Past(1)))
                .RuleFor(l => l.DueDate, (f, l) => l.LoanDate.AddDays(14))
                .RuleFor(l => l.ReturnedDate, (f, l) => f.Random.Bool() ? null : DateOnly.FromDateTime(f.Date.Recent()));

            var loans = loanFaker.Generate(15);
            context.Loans.AddRange(loans);
            context.SaveChanges();

            // ---------- ADMIN ROLE + USER SEED ----------

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string adminRole = "Admin";
            string adminEmail = "admin@library.com";
            string adminPassword = "Admin123!";

            // Create Admin role if it doesn't exist
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Create Admin user if it doesn't exist
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }

    }
}