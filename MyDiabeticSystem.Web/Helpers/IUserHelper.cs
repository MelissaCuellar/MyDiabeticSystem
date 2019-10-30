using Microsoft.AspNetCore.Identity;
using MyDiabeticSystem.Web.Data.Entities;
using MyDiabeticSystem.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IQueryable<Patient>> GetPatienssAsync(string userName);

        Task<IQueryable<Ratio>> GetRatiossAsync(string userName);
    }
}
