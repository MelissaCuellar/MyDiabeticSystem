using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiabeticSystem.Web.Data;
using MyDiabeticSystem.Web.Data.Entities;
using MyDiabeticSystem.Web.Helpers;
using MyDiabeticSystem.Web.Models;

namespace MyDiabeticSystem.Web.Controllers
{
    public class ChecksController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public ChecksController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        // GET: Checks
        public async Task<IActionResult> Index()
        {
            var model = await _userHelper.GetCheckssAsync(this.User.Identity.Name);

            return View(model);
        }

        // GET: Checks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _dataContext.Checks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // GET: Checks/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var patient = await this.GetPatientAsync(user.Id);

            
            double hb = 0, sum=0;

            var cont =  _dataContext.Checks
                .Include(p => p.Patient)
                .Where(p => p.Patient.Id == patient.Id)
                .Count();
            
            if (cont!=0)
            {
                var checks = _dataContext.Checks
                .Include(p => p.Patient)
                .Where(p => p.Patient.Id == patient.Id);

                foreach (var i in checks)
                {
                    sum += i.Glucometry;
                }
                hb = sum / cont;
            }


            var model = new AddCheckViewModel
            {
                PatientId = patient.Id,
                Hb1=hb,
            };

            return View(model);
        }

        // POST: Checks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCheckViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                double objective = user.Objective;

                double sensibility = 0;
                var sensibilities =  _dataContext.Sensibilities
                .Include(p => p.Patient)
                .Where(p => p.Patient.Id == model.PatientId);
                foreach(var s in sensibilities)
                {
                    if(DateTime.Now.Hour>=s.StartTime.Hour && DateTime.Now.Hour<=s.EndTime.Hour)
                    {
                        sensibility = s.Value;
                    }
                }

                double ratio = 0;
                var ratios = _dataContext.Ratios
                .Include(p => p.Patient)
                .Where(p => p.Patient.Id == model.PatientId);
                foreach (var r in ratios)
                {
                    if (DateTime.Now.Hour >= r.StartTime.Hour && DateTime.Now.Hour <= r.EndTime.Hour)
                    {
                        ratio = r.Value;
                    }
                }

                double hb = 0, sum = 0;

                var cont = _dataContext.Checks
                    .Include(p => p.Patient)
                    .Where(p => p.Patient.Id == model.PatientId)
                    .Count();
                if (cont != 0)
                {
                    var checks = _dataContext.Checks
                    .Include(p => p.Patient)
                    .Where(p => p.Patient.Id == model.PatientId);

                    foreach (var i in checks)
                    {
                        sum += i.Glucometry;
                    }
                    sum += model.Glucometry;
                    hb = sum / cont+1;
                }
                else
                {
                    hb += model.Glucometry;
                }

                double bolo = ((model.Glucometry - objective) / sensibility) + (model.Carbohydrates / ratio);

                var check = new Check
                {
                    Carbohydrates = model.Carbohydrates,
                    Glucometry = model.Glucometry,
                    Date = DateTime.Now,
                    Bolus = bolo,
                    Hb1 = hb,
                    Patient = await _dataContext.Patients.FindAsync(model.PatientId),
                };

                _dataContext.Checks.Add(check);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<Patient> GetPatientAsync(string id)
        {
            return await _dataContext.Patients
            .Include(o => o.User)
            .Where(o => o.User.Id.Equals(id))
            .FirstOrDefaultAsync();

        }

        // GET: Checks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _dataContext.Checks.FindAsync(id);
            if (check == null)
            {
                return NotFound();
            }
            return View(check);
        }

        // POST: Checks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Carbohydrates,Glucometry,Date,Bolus,Hb1")] Check check)
        {
            if (id != check.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(check);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckExists(check.Id))
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
            return View(check);
        }

        // GET: Checks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _dataContext.Checks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // POST: Checks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var check = await _dataContext.Checks.FindAsync(id);
            _dataContext.Checks.Remove(check);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckExists(int id)
        {
            return _dataContext.Checks.Any(e => e.Id == id);
        }
    }
}
