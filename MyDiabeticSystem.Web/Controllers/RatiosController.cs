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
    public class RatiosController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;


        public RatiosController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;

        }

        // GET: Ratios
        public async Task<IActionResult> Index()
        {
            var model = await _userHelper.GetRatiossAsync(this.User.Identity.Name);
            return View(model);
        }

        // GET: Ratios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratio = await _dataContext.Ratios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratio == null)
            {
                return NotFound();
            }

            return View(ratio);
        }

        // GET: Ratios/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var patient = await this.GetPatientAsync(user.Id);

            var model = new RatioViewModel
            {
                PatientId = patient.Id
            };
           
            return View(model);
        }

        // POST: Ratios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RatioViewModel view)
        {
            
            if (ModelState.IsValid)
            {

                var ratio = new Ratio
                {
                    StartTime = view.StartTime,
                    EndTime = view.EndTime,
                    Value = view.Value,
                    Patient = await _dataContext.Patients.FindAsync(view.PatientId),
                };
                _dataContext.Ratios.Add(ratio);
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

        // GET: Ratios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            /*if(user.CanEdit==false)
            {
                ModelState.AddModelError(string.Empty, "You can't edit.");
                return NotFound();
            }*/
            if (id == null)
            {
                return NotFound();
            }

            var ratio = await _dataContext.Ratios.FindAsync(id);
            if (ratio == null)
            {
                return NotFound();
            }
            return View(ratio);
        }

        // POST: Ratios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,Value")] Ratio ratio)
        {
            if (id != ratio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(ratio);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatioExists(ratio.Id))
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
            return View(ratio);
        }

        // GET: Ratios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratio = await _dataContext.Ratios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratio == null)
            {
                return NotFound();
            }

            return View(ratio);
        }

        // POST: Ratios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratio = await _dataContext.Ratios.FindAsync(id);
            _dataContext.Ratios.Remove(ratio);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatioExists(int id)
        {
            return _dataContext.Ratios.Any(e => e.Id == id);
        }
    }
}
