using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiabeticSystem.Web.Data;
using MyDiabeticSystem.Web.Data.Entities;
using MyDiabeticSystem.Web.Helpers;
using MyDiabeticSystem.Web.Models;

namespace MyDiabeticSystem.Web.Controllers
{
    [Authorize(Roles ="Manager")]
    public class DoctorsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public DoctorsController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }
        

        // GET: Doctors
        public IActionResult Index()
        {
            return View(_dataContext.Doctors
                .Include(o => o.User));
        }


        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _dataContext.Doctors
                .Include(o => o.User)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var user = await AddUser(view);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(view);
                }
                var doctor = new Doctor
                {
                    User = user,
                };
                _dataContext.Doctors.Add(doctor);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }
        private async Task<User> AddUser(AddUserViewModel view)
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

            var result = await _userHelper.AddUserAsync(user, view.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await _userHelper.GetUserByEmailAsync(view.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Doctor");
            return newUser;
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _dataContext.Doctors
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (doctor == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Document = doctor.User.Document,
                FirstName = doctor.User.FirstName,
                LastName = doctor.User.LastName,
                Id = doctor.Id,
                PhoneNumber = doctor.User.PhoneNumber,
            };
            return View(model);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {            
            if (ModelState.IsValid)
            {
                var doctor = await _dataContext.Doctors
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                doctor.User.Document = model.Document;
                doctor.User.FirstName = model.FirstName;
                doctor.User.LastName = model.LastName;
                doctor.User.PhoneNumber = model.PhoneNumber;

                await _userHelper.UpdateUserAsync(doctor.User);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _dataContext.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _dataContext.Doctors.FindAsync(id);
            _dataContext.Doctors.Remove(doctor);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _dataContext.Doctors.Any(e => e.Id == id);
        }
    }
}
