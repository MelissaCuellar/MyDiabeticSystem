using MyDiabeticSystem.Web.Data.Entities;
using MyDiabeticSystem.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1146437549", "Melissa", "Cuellar", "meli0117@gmail.com", DateTime.Parse("01/17/1994"),"301 474 7485", "Manager",true,1);
            var doctor = await CheckUserAsync("1146437549", "Melissa", "Cuellar", "melissacuellar208847@correo.itm.edu.co", DateTime.Parse("01/17/1994"), "301 474 7485", "Doctor", true,1);
            
            await CheckDoctorsAsync(doctor);
            
            await CheckManagerAsync(manager);

        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckDoctorsAsync(User user)
        {
            if (!_context.Doctors.Any())
            {
                _context.Doctors.Add(new Doctor { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Doctor");
            await _userHelper.CheckRoleAsync("Patient");
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            DateTime dateBirth,
            string phone,
            string role,
            bool canEdit,
            double objetive)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Document = document,
                    DateBirth = dateBirth,
                    CanEdit=canEdit,
                    Objective=objetive,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

    }
}
