using JobApplicationManager.Data;
using JobApplicationManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationManager.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all job applications
        public async Task<IActionResult> Index()
        {
            var applications = await _context.JobApplications
                .OrderByDescending(a => a.ApplicationDate)
                .ToListAsync();

            return View(applications);
        }

        // Display application details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.JobApplications
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // Show the create form
        public IActionResult Create()
        {
            return View();
        }

        // Save a new application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("CompanyName,JobTitle,ApplicationDate,ContactEmail,Status,Notes")]
            JobApplication application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(application);
        }

        // Show the edit form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.JobApplications.FindAsync(id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // Save changes to an application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,CompanyName,JobTitle,ApplicationDate,ContactEmail,Status,Notes")]
            JobApplication application)
        {
            if (id != application.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(application);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(application);
        }

        // Show delete confirmation
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.JobApplications
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // Delete an application
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);

            if (application != null)
            {
                _context.JobApplications.Remove(application);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}