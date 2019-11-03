using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiabeticSystem.Web.Data;
using MyDiabeticSystem.Web.Data.Entities;
using MyDiabeticSystem.Web.Helpers;
using MyDiabeticSystem.Web.Models;

namespace MyDiabeticSystem.Web.Controllers
{
    [Authorize(Roles = "Patient")]
    public class SensibilitiesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SensibilitiesController(
            DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }

        // GET: Sensibilities
        public async Task<IActionResult> Index()
        {
            var model = await _userHelper.GetSencibilitiessAsync(this.User.Identity.Name);
            return View(model);
        }

        // GET: Sensibilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensibility = await _dataContext.Sensibilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sensibility == null)
            {
                return NotFound();
            }

            return View(sensibility);
        }

        // GET: Sensibilities/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var patient = await this.GetPatientAsync(user.Id);

            var model = new AddSensibilityViewModel
            {
                PatientId = patient.Id
            };

            return View(model);
        }

        // POST: Sensibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddSensibilityViewModel view)
        {
            if (ModelState.IsValid)
            {

                var sensibility = new Sensibility
                {
                    StartTime = view.StartTime,
                    EndTime = view.EndTime,
                    Value = view.Value,
                    Patient = await _dataContext.Patients.FindAsync(view.PatientId),
                };
                _dataContext.Sensibilities.Add(sensibility);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        public async Task<Patient> GetPatientAsync(string id)
        {
            return await _dataContext.Patients
            .Include(o => o.User)
            .Where(o => o.User.Id.Equals(id))
            .FirstOrDefaultAsync();

        }

        // GET: Sensibilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensibility = await _dataContext.Sensibilities.FindAsync(id);
            if (sensibility == null)
            {
                return NotFound();
            }
            return View(sensibility);
        }

        // POST: Sensibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,Value")] Sensibility sensibility)
        {
            if (id != sensibility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(sensibility);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensibilityExists(sensibility.Id))
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
            return View(sensibility);
        }

        // GET: Sensibilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensibility = await _dataContext.Sensibilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sensibility == null)
            {
                return NotFound();
            }

            return View(sensibility);
        }

        // POST: Sensibilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensibility = await _dataContext.Sensibilities.FindAsync(id);
            _dataContext.Sensibilities.Remove(sensibility);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensibilityExists(int id)
        {
            return _dataContext.Sensibilities.Any(e => e.Id == id);
        }
    }
}
