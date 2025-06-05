using Clubify.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clubify.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); 
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


            // Create Admin role if it doesn't exist
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Create Admin user if it doesn't exist
            var adminEmail = "admin@clubify.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Change password in production

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    // Log errors here if needed
                }
            }

            // Seed Memberships if none exist
            if (!await dbContext.Memberships.AnyAsync())
            {
                var firstClub = await dbContext.Clubs.FirstOrDefaultAsync();
                if (firstClub != null && adminUser != null)
                {
                    var membership = new Membership
                    {
                        UserId = adminUser.Id,
                        ClubId = firstClub.Id,
                        JoinedOn = DateTime.UtcNow.AddMonths(-3),
                        ExpiryDate = DateTime.UtcNow.AddMonths(9),
                        IsActive = true
                    };

                    dbContext.Memberships.Add(membership);
                    await dbContext.SaveChangesAsync();

                    // Seed Payments for this membership
                    var payments = new List<Payment>
                    {
                        new Payment
                        {
                            MembershipId = membership.Id,
                            Amount = 50.00m,
                            PaymentDate = DateTime.UtcNow.AddMonths(-3),
                            PaymentMethod = "Cash",
                            Notes = "Initial payment"
                        },
                        new Payment
                        {
                            MembershipId = membership.Id,
                            Amount = 25.00m,
                            PaymentDate = DateTime.UtcNow.AddMonths(-1),
                            PaymentMethod = "Bank Transfer",
                            Notes = "Monthly renewal"
                        }
                    };

                    dbContext.Payments.AddRange(payments);
                    await dbContext.SaveChangesAsync();
                }
            }

            // Seed LicenseTiers if none exist
            if (!await dbContext.LicenseTiers.AnyAsync())
            {
                var tiers = new List<LicenseTier>
                {
                    new LicenseTier { Name = "Free", MemberCap = 5, MonthlyPrice = 0m, AnnualPrice = 0m, Notes = "Limited features" },
                    new LicenseTier { Name = "Starter", MemberCap = 25, MonthlyPrice = 15m, AnnualPrice = 150m, Notes = "Essential features" },
                    new LicenseTier { Name = "Pro", MemberCap = 100, MonthlyPrice = 35m, AnnualPrice = 350m, Notes = "Advanced features" },
                    new LicenseTier { Name = "Enterprise", MemberCap = int.MaxValue, MonthlyPrice = 0m, AnnualPrice = 0m, Notes = "Custom Quote, unlimited members" }
                };

                dbContext.LicenseTiers.AddRange(tiers);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
