using MyProject.Models.Account;

namespace MyProject.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(UserModel user);
        Task<UserModel> ValidateUserAsync(string username, string password);

    }
}
