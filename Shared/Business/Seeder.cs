using Entities.Models;
using Repositories;

namespace Business
{
    public static class Seeder
    {
        public static async Task SeedInitialUser(IUnitOfWork unitOfWork)
        {
            var adminEmail = "admin@example.com";
            var userRepo = unitOfWork.Repository<User>();
            var adminUser = await userRepo.FindAsync(u => u.Email == adminEmail);

            if (adminUser == null || adminUser.Count() == 0)
            {
                var newAdminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userRepo.AddAsync(newAdminUser);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}
