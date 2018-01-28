using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JHUProject.Daos;
using JHUProject.Models;

namespace JHUProject.Controllers
{
    public class CliniciansController : Controller
    {
        private readonly JHUContext _context;

        public CliniciansController(JHUContext context)
        {
            _context = context;
        }

        // GET: Clinicians
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clinicians.ToListAsync());
        }

        // GET: Clinicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinician = await _context.Clinicians
                .SingleOrDefaultAsync(m => m.ClinicianID == id);
            if (clinician == null)
            {
                return NotFound();
            }

            return View(clinician);
        }

        // GET: Clinicians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClinicianID,FirstName,LastName,Address,PhoneNumber,CreatedDate")] Clinician clinician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clinician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinician);
        }

        // GET: Clinicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinician = await _context.Clinicians.SingleOrDefaultAsync(m => m.ClinicianID == id);
            if (clinician == null)
            {
                return NotFound();
            }
            return View(clinician);
        }

        // POST: Clinicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClinicianID,FirstName,LastName,Address,PhoneNumber,CreatedDate")] Clinician clinician)
        {
            if (id != clinician.ClinicianID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicianExists(clinician.ClinicianID))
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
            return View(clinician);
        }

        // GET: Clinicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinician = await _context.Clinicians
                .SingleOrDefaultAsync(m => m.ClinicianID == id);
            if (clinician == null)
            {
                return NotFound();
            }

            return View(clinician);
        }

        // POST: Clinicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var clinician = await _context.Clinicians.SingleOrDefaultAsync(m => m.ClinicianID == id);
            if(_context.Biopsies.Any(b => b.ClinicianID == id))
            {
                ModelState.AddModelError("ClinicianID", "Cannot delete clinician record: there are biopsies records that refering to this clinician");
                return View("Delete",clinician);
            }
            _context.Clinicians.Remove(clinician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicianExists(int id)
        {
            return _context.Clinicians.Any(e => e.ClinicianID == id);
        }
    }
}
