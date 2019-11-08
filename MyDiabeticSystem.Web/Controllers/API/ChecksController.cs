using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDiabeticSystem.Web.Data;
using MyDiabeticSystem.Web.Data.Entities;

namespace MyDiabeticSystem.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecksController : ControllerBase
    {
        private readonly DataContext _context;

        public ChecksController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Checks
        [HttpGet]
        public IEnumerable<Check> GetChecks()
        {
            return _context.Checks;
        }

        // GET: api/Checks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheck([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var check = await _context.Checks.FindAsync(id);

            if (check == null)
            {
                return NotFound();
            }

            return Ok(check);
        }

        // PUT: api/Checks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheck([FromRoute] int id, [FromBody] Check check)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != check.Id)
            {
                return BadRequest();
            }

            _context.Entry(check).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Checks
        [HttpPost]
        public async Task<IActionResult> PostCheck([FromBody] Check check)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Checks.Add(check);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheck", new { id = check.Id }, check);
        }

        // DELETE: api/Checks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheck([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var check = await _context.Checks.FindAsync(id);
            if (check == null)
            {
                return NotFound();
            }

            _context.Checks.Remove(check);
            await _context.SaveChangesAsync();

            return Ok(check);
        }

        private bool CheckExists(int id)
        {
            return _context.Checks.Any(e => e.Id == id);
        }
    }
}