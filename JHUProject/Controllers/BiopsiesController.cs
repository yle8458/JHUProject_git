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
    public class BiopsiesController : Controller
    {
        private readonly JHUContext _context;

        public BiopsiesController(JHUContext context)
        {
            _context = context;
        }

        // GET: Biopsies
        public async Task<IActionResult> Index()
        {
            var jHUContext = _context.Biopsies.Include(b => b.Clinician).Include(b => b.Patient);
            return View(await jHUContext.ToListAsync());
        }

        
        // GET: Biopsies/BiopsyList?patientID=1
        public ActionResult BiopsyList(int? patientID)
        {
            //System.Diagnostics.Debug.WriteLine("test");
            if (patientID == null)
            {
                return NotFound();
            }
            var biopsies = _context.Biopsies.Include(b => b.Clinician).Include(b => b.Patient).Where(b => b.PatientID == patientID);
            var patient = _context.Patients.SingleOrDefault(p => p.PatientID == patientID);
            if(patient == null)
            {
                return RedirectToAction("Error","Home");
            }
            ViewData["PatientName"] = patient.FullName;
            ViewData["PatientID"] = patientID;
            return View(biopsies.ToList());
        }

        // GET: Biopsies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biopsy = await _context.Biopsies
                .Include(b => b.Clinician)
                .Include(b => b.Patient)
                .SingleOrDefaultAsync(m => m.BiopsyID == id);
            if (biopsy == null)
            {
                return NotFound();
            }

            return View(biopsy);
        }

        // GET: Biopsies/Create
        public IActionResult Create()
        {
            ViewData["ClinicianID"] = new SelectList(_context.Clinicians, "ClinicianID", "FullName", null);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", null);
            //var patient =  _context.Patients.SingleOrDefault(p => p.PatientID == patientID);
            //ViewData["PatientName"] = patient.FullName;
            return View();
        }

        // GET: Biopsies/CreateWithID?
        public IActionResult CreateWithID(int? patientID)
        {
            ViewData["ClinicianID"] = new SelectList(_context.Clinicians, "ClinicianID", "FullName",null);
            //ViewData["PatientID"] = new SelectList(_context.Patients.Where(p=>p.PatientID == patientID), "PatientID", "FullName", patientID);
            Patient patient = _context.Patients.SingleOrDefault(p => p.PatientID == patientID);
            //var patient =  _context.Patients.SingleOrDefault(p => p.PatientID == patientID);
            if (patient == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["PatientName"] = patient.FullName;
            ViewData["PatientID"] = patient.PatientID;
            return View();
        }

        // POST: Biopsies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BiopsyID,PatientID,ClinicianID,RecordNumber,ServiceDate,BiopsySite,CreatedDate")] Biopsy biopsy)
        {
            if (_context.Biopsies.Any(b => b.RecordNumber.Equals(biopsy.RecordNumber)))
            {
                ModelState.AddModelError("RecordNumber", "The Record Number existed in the system");
            }

            if (ModelState.IsValid)
            {
                
                _context.Add(biopsy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClinicianID"] = new SelectList(_context.Clinicians, "ClinicianID", "FullName", biopsy.ClinicianID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", biopsy.PatientID);

            Patient patient = _context.Patients.SingleOrDefault(p => p.PatientID == biopsy.PatientID);
            //var patient =  _context.Patients.SingleOrDefault(p => p.PatientID == patientID);
            if (patient == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["PatientName"] = patient.FullName;
            return View(biopsy);
        }

        // POST: Biopsies/CreateWithID
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithID([Bind("BiopsyID,PatientID,ClinicianID,RecordNumber,ServiceDate,BiopsySite,CreatedDate")] Biopsy biopsy)
        {
            if (_context.Biopsies.Any(b => b.RecordNumber.Equals(biopsy.RecordNumber)))
            {
                ModelState.AddModelError("RecordNumber", "The Record Number existed in the system");
            }

            if (ModelState.IsValid)
            {

                _context.Add(biopsy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BiopsyList), new { patientID = biopsy.PatientID });
            }
            ViewData["ClinicianID"] = new SelectList(_context.Clinicians, "ClinicianID", "FullName", biopsy.ClinicianID);
            
            Patient patient = _context.Patients.SingleOrDefault(p => p.PatientID == biopsy.PatientID);
            if (patient == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["PatientName"] = patient.FullName;
            ViewData["PatientID"] = patient.PatientID;
            return View(biopsy);
        }

        // GET: Biopsies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biopsy = await _context.Biopsies.SingleOrDefaultAsync(m => m.BiopsyID == id);
            if (biopsy == null)
            {
                return NotFound();
            }
            ViewData["ClinicianID"] = new SelectList(_context.Clinicians, "ClinicianID", "FullName", biopsy.ClinicianID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", biopsy.PatientID);
            return View(biopsy);
        }

        // POST: Biopsies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BiopsyID,PatientID,ClinicianID,RecordNumber,ServiceDate,BiopsySite,CreatedDate")] Biopsy biopsy)
        {
            if (id != biopsy.BiopsyID)
            {
                return NotFound();
            }
            if (_context.Biopsies.Any(b => b.RecordNumber.Equals(biopsy.RecordNumber)))
            {
                ModelState.AddModelError("RecordNumber", "The Record Number existed in the system");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biopsy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiopsyExists(biopsy.BiopsyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details),new { id = biopsy.BiopsyID});
            }
            ViewData["ClinicianID"] = new SelectList(_context.Clinicians, "ClinicianID", "FullName", biopsy.ClinicianID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", biopsy.PatientID);
            return View(biopsy);
        }

        // GET: Biopsies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biopsy = await _context.Biopsies
                .Include(b => b.Clinician)
                .Include(b => b.Patient)
                .SingleOrDefaultAsync(m => m.BiopsyID == id);
            if (biopsy == null)
            {
                return NotFound();
            }

            return View(biopsy);
        }

        // POST: Biopsies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biopsy = await _context.Biopsies.SingleOrDefaultAsync(m => m.BiopsyID == id);
            _context.Biopsies.Remove(biopsy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BiopsyList),new { patientID = biopsy.PatientID});
        }

        private bool BiopsyExists(int id)
        {
            return _context.Biopsies.Any(e => e.BiopsyID == id);
        }
    }
}
