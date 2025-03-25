using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentBookingAjaxJquery.Models;

namespace AppointmnetBookingAjaxJquery.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly AppBookDBContext _context;

        public DoctorsController(AppBookDBContext context)
        {
            _context = context;
        }

        // GET: Doctor
        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return View(doctors);
        }


        // GET: Doctor/AddOrEdit (Create or Edit Modal)
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Doctor());

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }


        // POST: Doctor/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("DoctorId,DoctorName,Specialization,Contact,Email")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(doctor);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DoctorExists(doctor.DoctorId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                var doctors = await _context.Doctors.ToListAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", doctors) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", doctor) });
        }

        // POST: Doctor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Doctors.ToList()) });
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
