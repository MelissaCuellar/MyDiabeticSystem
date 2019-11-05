using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiabeticSystem.Web.Data;
using MyDiabeticSystem.Web.Data.Entities;
using MyDiabeticSystem.Web.Helpers;
using MyDiabeticSystem.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyDiabeticSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public AccountController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var model = new AddUserViewModel
            {
                Roles = _userHelper.GetComboRoles()
            };

            return View(model);
        }

        public async Task<IActionResult> Create(AddUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var role = "Patient";
                bool canEdit = false;


                if(view.RoleId==1)
                {
                    role = "Doctor";
                    canEdit = true;
                }

                var user = await _userHelper.AddUser(view, role,canEdit);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(view);
                }

                if(view.RoleId==1)
                {
                    var doctor = new Doctor
                    {
                        User = user,                        
                    };
                    _dataContext.Doctors.Add(doctor);
                }
                else
                {
                    var patient = new Patient
                    {
                        User = user,                       

                    };
                    _dataContext.Patients.Add(patient);
                }
                
                await _dataContext.SaveChangesAsync();

            }
            return View(view);
        }

        public async Task<IActionResult> ChangeUserDoctor()
        {

            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var doctor = await this.GetDoctorAsync(user.Id);
            

            if (user == null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserDoctor(EditUserViewModel model)
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
                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }

        public async Task<Doctor> GetDoctorAsync(string id)
        {
            return await _dataContext.Doctors
            .Include(o => o.User)
            .Where(o => o.User.Id.Equals(id))
            .FirstOrDefaultAsync();

        }


        public async Task<IActionResult> ChangeUserPatient()
        {

            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var patient = await this.GetPatientAsync(user.Id);

            var model = new EditPatientViewModel();

            if (user != null)
            {
                model.Document = user.Document;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Id = patient.Id;
                model.PhoneNumber = user.PhoneNumber;
                model.FathersEmail = user.FathersEmail;
                model.DateBirth = user.DateBirth;
                model.Objective = user.Objective;

                if(patient.Doctor==null)
                {
                    model.Doctors = GetComboDoctors();
                }
                else
                {
                    model.Doctors = GetComboDoctors();
                    model.DoctorId = patient.Doctor.Id;
                }
            }

            /*var model = new EditPatientViewModel
            {
                Document = patient.User.Document,
                FirstName = patient.User.FirstName,
                LastName = patient.User.LastName,
                Id = patient.Id,
                PhoneNumber = patient.User.PhoneNumber,
                Doctors = GetComboDoctors(),
            };*/


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserPatient(EditPatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = await _dataContext.Patients
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                patient.User.Document = model.Document;
                patient.User.FirstName = model.FirstName;
                patient.User.LastName = model.LastName;
                patient.User.PhoneNumber = model.PhoneNumber;
                patient.User.FathersEmail = model.FathersEmail;
                patient.User.DateBirth = model.DateBirth;
                patient.Doctor = await _dataContext.Doctors.FindAsync(model.DoctorId);
                patient.User.Objective = model.Objective;

                await _userHelper.UpdateUserAsync(patient.User);
                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }



        public async Task<Patient> GetPatientAsync(string id)
        {
            return await _dataContext.Patients
            .Include(o => o.User)
            .Include(o => o.Doctor)
            .Where(o => o.User.Id.Equals(id))
            .FirstOrDefaultAsync();

        }

        public IEnumerable<SelectListItem> GetComboDoctors()
        {
            var list = _dataContext.Doctors.Include(p => p.User).Select(p => new SelectListItem
            {
                Text = p.User.FullName,
                Value = p.Id.ToString()

            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select your doctor...)",
                Value = "0"
            });

            return list;
        }

       
    }
}
