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
    [Authorize(Roles = "Doctor")]
    public class PatientsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;


        public PatientsController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
           
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var model = await _userHelper.GetPatienssAsync(this.User.Identity.Name);
                       
            return View(model);
        }

        public async Task<IActionResult> CreateParameter(int id)
        {

            var model = new AddParameterViewModel
            {
                PatientId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParameter(AddParameterViewModel view)
        {
            if (ModelState.IsValid)
            {
                int idP = view.PatientId;

                var parameter = new Parameter
                {
                    Description= view.Description,
                    StartTime = view.StartTime,
                    EndTime = view.EndTime,
                    Value = view.Value,
                    Patient = await _dataContext.Patients.FindAsync(view.PatientId),
                };
                _dataContext.Parameters.Add(parameter);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("DetailParameter/" + idP);
            }
            return View(view);
        }

        public async Task<IActionResult> CreateSensibility(int id)
        {
           
            var model = new AddSensibilityViewModel
            {
                PatientId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSensibility(AddSensibilityViewModel view)
        {
            if (ModelState.IsValid)
            {
                int idP = view.PatientId;

                var sensibility = new Sensibility
                {
                    StartTime = view.StartTime,
                    EndTime = view.EndTime,
                    Value = view.Value,
                    Patient = await _dataContext.Patients.FindAsync(view.PatientId),
                };
                _dataContext.Sensibilities.Add(sensibility);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("DetailSensibility/"+idP);
            }
            return View(view);
        }

        public async Task<IActionResult> CreateRatio(int id)
        {

            var model = new RatioViewModel
            {
                PatientId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRatio(RatioViewModel view)
        {
            if (ModelState.IsValid)
            {
                int idP = view.PatientId;

                var ratio = new Ratio
                {
                    StartTime = view.StartTime,
                    EndTime = view.EndTime,
                    Value = view.Value,
                    Patient = await _dataContext.Patients.FindAsync(view.PatientId),
                };
                _dataContext.Ratios.Add(ratio);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("DetailRatio/" + idP);
            }
            return View(view);
        }


        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dataContext.Patients
                .Include(o => o.User)
                .Include(o => o.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }


        public async Task<IActionResult> DetailRatio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cont = _dataContext.Ratios
                .Include(o => o.Patient)
                .Where(o => o.Patient.Id == id)
                .Count();
            if (cont==0)
            {
                return RedirectToAction("CreateRatio/" + id);
            }
            var model = await _userHelper.GetRatiossAsync(id);            
            
            return View(model);
        }

        public async Task<IActionResult> DetailParameter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cont = _dataContext.Parameters
                .Include(o => o.Patient)
                .Where(o => o.Patient.Id == id)
                .Count();
            if (cont == 0)
            {
                return RedirectToAction("CreateParameter/" + id);
            }
            var model = await _userHelper.GetParameterssAsync(id);

            return View(model);
        }

        public async Task<IActionResult> DetailSensibility(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cont = _dataContext.Sensibilities
               .Include(o => o.Patient)
               .Where(o => o.Patient.Id == id)
               .Count();
            if (cont == 0)
            {
                return RedirectToAction("CreateSensibility/" + id);
            }
            var model = await _userHelper.GetSencibilitiessAsync(id);

            return View(model);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            var model = new AddPatientViewModel
            {
                Doctors = GetComboDoctors()
            };
            return this.View(model);
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

        

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddPatientViewModel view)
        {
            if (ModelState.IsValid)
            {
                var user = await AddUser(view);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(view);
                }
                var patient = new Patient
                {
                    User = user,
                    Doctor = await _dataContext.Doctors.FindAsync(view.DoctorId)

                };
                _dataContext.Patients.Add(patient);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
               
            }
            return View(view);
        }

        private async Task<User> AddUser(AddPatientViewModel view)
        {
            var user = new User
            {               
                Document = view.Document,
                Email = view.Username,
                FirstName = view.FirstName,
                LastName = view.LastName,
                PhoneNumber = view.PhoneNumber,
                UserName = view.Username,
                DateBirth = view.DateBirth,
                FathersEmail = view.FathersEmail,
                CanEdit = false,

            };
            var result = await _userHelper.AddUserAsync(user, view.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }
            var newUser = await _userHelper.GetUserByEmailAsync(view.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Patient");
            return newUser;

        }

        public async Task<IActionResult> ChangeUserPatient(int id)
        {           
            var patient = await this.GetPatientAsync(id);

            var model = new EditPatientViewModel();

            if (patient != null)
            {
                model.Document = patient.User.Document;
                model.FirstName = patient.User.FirstName;
                model.LastName = patient.User.LastName;
                model.Id = patient.Id;
                model.PhoneNumber = patient.User.PhoneNumber;
                model.FathersEmail = patient.User.FathersEmail;
                model.DateBirth = patient.User.DateBirth;
                model.CanEdit = patient.User.CanEdit;

                if (patient.Doctor == null)
                {
                    model.Doctors = GetComboDoctors();
                }
                else
                {
                    model.Doctors = GetComboDoctors();
                    model.DoctorId = patient.Doctor.Id;
                }
            }

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
                patient.User.CanEdit = model.CanEdit;

                var id = patient.Id;

                await _userHelper.UpdateUserAsync(patient.User);
                return RedirectToAction("Details/" + id);

                

            }
            return View(model);
        }
        public async Task<Patient> GetPatientAsync(int id)
        {
            return await _dataContext.Patients
            .Include(o => o.User)
            .Include(o => o.Doctor)
            .Where(o => o.Id==id)
            .FirstOrDefaultAsync();

        }
        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dataContext.Patients
                .Include(o => o.User)
                .Include(o => o.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateBirth,CanEdit")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(patient);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dataContext.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _dataContext.Patients.FindAsync(id);
            _dataContext.Patients.Remove(patient);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _dataContext.Patients.Any(e => e.Id == id);
        }
    }
}
