using MyProject.Data;
using MyProject.Models.Account;

using Microsoft.EntityFrameworkCore;

namespace MyProject.Services
{
  

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task CreateUserAsync(UserModel user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
        public async Task<UserModel> ValidateUserAsync(string username, string password)
        {
           
            return await context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
