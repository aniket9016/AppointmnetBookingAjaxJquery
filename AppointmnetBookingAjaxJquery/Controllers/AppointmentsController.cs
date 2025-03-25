using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentBookingAjaxJquery.Models;
using AppointmnetBookingAjaxJquery;

namespace AppointmentBookingAjaxJquery.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppBookDBContext _context;

        public AppointmentsController(AppBookDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchkey)
        {
            var appointments = _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).AsQueryable();
            if(!string.IsNullOrEmpty(searchkey))
            {
                searchkey = searchkey.ToLower();
                appointments = appointments.Where(a =>
                    a.Patient.PatientName.ToLower().Contains(searchkey) ||
                    a.Doctor.DoctorName.ToLower().Contains(searchkey));
            }
            ViewData["searchkey"] = searchkey;
            return View(await appointments.ToListAsync());
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();

            if (id == 0)
                return View(new Appointment());
            else
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null) return NotFound();
                return View(appointment);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,PatientId,DoctorId,AppointmentDate,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(appointment);
                }
                else
                {
                    _context.Update(appointment);
                }
                await _context.SaveChangesAsync();
                var appointments = await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToListAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", appointments) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", appointment) });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            var appointments = await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToListAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", appointments) });
        }
    }
}
