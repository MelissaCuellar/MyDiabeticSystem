using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiabeticSystem.Web.Data;
using MyDiabeticSystem.Web.Data.Entities;
using MyDiabeticSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _dataContext;

        public UserHelper(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            DataContext dataContext)

        {
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<User> AddUser(AddUserViewModel view, string role)
        {
            var user = new User
            {
                Document = view.Document,
                Email = view.Username,
                FirstName = view.FirstName,
                LastName = view.LastName,
                PhoneNumber = view.PhoneNumber,
                UserName = view.Username,
            };
            var result = await AddUserAsync(user, view.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await GetUserByEmailAsync(view.Username);
            await AddUserToRoleAsync(newUser, role);
            return newUser;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IQueryable<Check>> GetCheckssAsync(string userName)
        {
            var user = await GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }
            return this._dataContext.Checks
                .Include(o => o.Patient)
                .Where(o => o.Patient.User == user);
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "(Select a role...)" },
                new SelectListItem { Value = "1", Text = "Doctor" },
                new SelectListItem { Value = "2", Text = "Patient" }
            };

            return list;
        }

        public async Task<IQueryable<Patient>> GetPatienssAsync(string userName)
        {
            var user = await GetUserByEmailAsync(userName);
            if (user==null)
            {
                return null;
            }
                        
                return this._dataContext.Patients
                .Include(o => o.User)
                .Include(o => o.Doctor)
                .Where(o => o.Doctor.User == user);
            
        }

        public async Task<IQueryable<Ratio>> GetRatiossAsync(string userName)
        {
            var user = await GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }
            return this._dataContext.Ratios
                .Include(o => o.Patient)
                .Where(o => o.Patient.User == user);

                
        }

        public async Task<IQueryable<Sensibility>> GetSencibilitiessAsync(string userName)
        {
            var user = await GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }
            return this._dataContext.Sensibilities
                .Include(o => o.Patient)
                .Where(o => o.Patient.User == user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
            model.Username,
            model.Password,
            model.RememberMe,
            false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
