using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoesShelf.Data;
using ShoesShelf.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShoesShelf.Controllers
{
    /// <summary>
    /// DisinfectionsController manages the CRUD operations and data access for shoe disinfections,
    /// enabling users to view, create, edit, and delete disinfection records associated with specific shoes.
    /// Includes pagination for listing records efficiently in the Index action.
    /// </summary>
    [Authorize(Roles = UserRoles.Admin)]
    public class DisinfectionsController : Controller
    {
        // Dependency Injection: Injects the database context to access the Disinfection data
        private readonly ApplicationDbContext _context;

        // Constructor to initialize ApplicationDbContext through dependency injection
        public DisinfectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Disinfections - Retrieves and displays a paginated list of disinfections
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            // Load all Disinfections along with related Shoe data
            var applicationDbContext = _context.Disinfection.Include(r => r.Shoe);

            // Define pagination settings
            int pageSize = 7;

            // Return paginated list of disinfections
            return View(await PaginatedList<Disinfection>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Disinfections/Details/5 - Shows details of a specific disinfection by Id
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            // If Id is null or Disinfection entity set is unavailable, return NotFound
            if (id == null || _context.Disinfection == null)
            {
                return NotFound();
            }

            // Retrieve Disinfection with Shoe data based on the provided Id
            var disinfection = await _context.Disinfection
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.Id == id);

            // If the specified Disinfection is not found, return NotFound
            if (disinfection == null)
            {
                return NotFound();
            }

            // Render the disinfection details view
            return View(disinfection);
        }

        // GET: Disinfections/Create - Renders a form for creating a new disinfection record
        public IActionResult Create()
        {
            // Populate dropdown with available Shoe Ids and their FullDefinition descriptions
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition");
            return View();
        }

        // POST: Disinfections/Create - Saves a new disinfection record to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, ShoeId, DisinfectionDate")] Disinfection disinfection)
        {
            // Check if the model state is valid before saving
            if (ModelState.IsValid)
            {
                // Add the new Disinfection entry and save changes
                _context.Add(disinfection);
                await _context.SaveChangesAsync();

                // Redirect to the Index action after successful creation
                return RedirectToAction(nameof(Index));
            }

            // If model validation fails, re-render the Create view with Shoe options
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition", disinfection.ShoeId);
            return View(disinfection);
        }

        // GET: Disinfections/Edit/5 - Retrieves a specific disinfection for editing
        public async Task<IActionResult> Edit(int? id)
        {
            // If Id is null or Disinfection entity set is unavailable, return NotFound
            if (id == null || _context.Disinfection == null)
            {
                return NotFound();
            }

            // Retrieve the specific Disinfection record by I
            var disinfection = await _context.Disinfection.FindAsync(id);

            // If the specified Disinfection is not found, return NotFound
            if (disinfection == null)
            {
                return NotFound();
            }

            // Populate dropdown with available Shoe Ids and their FullDefinition for selection
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition", disinfection.ShoeId);

            // Render the Edit view with the selected Disinfection data
            return View(disinfection);
        }

        // POST: Disinfections/Edit/5 - Updates an existing disinfection record in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ShoeId, DisinfectionDate")] Disinfection disinfection)
        {
            // Check if the Id of the disinfection being edited matches the provided Id
            if (id != disinfection.Id)
            {
                return NotFound();
            }

            // Validate the model before updating
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the Disinfection record and save changes
                    _context.Update(disinfection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issues by checking if the record still exists
                    if (!DisinfectionExists(disinfection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect to the Index view upon successful edit
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, reload dropdown and re-render Edit view
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition", disinfection.ShoeId);
            return View(disinfection);
        }

        // GET: Disinfections/Delete/5 - Confirms deletion of a specific disinfection record
        public async Task<IActionResult> Delete(int? id)
        {
            // If Id is null or Disinfection entity set is unavailable, return NotFound
            if (id == null || _context.Disinfection == null)
            {
                return NotFound();
            }

            // Retrieve the Disinfection by Id, including Shoe data for display
            var disinfection = await _context.Disinfection
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.Id == id);

            // If the specified Disinfection is not found, return NotFound
            if (disinfection == null)
            {
                return NotFound();
            }

            // Render the Delete view with the selected Disinfection data for confirmation
            return View(disinfection);
        }

        // POST: Disinfections/Delete/5 - Deletes the confirmed disinfection record from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Confirm that the Disinfection entity set exists
            if (_context.Disinfection == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Disinfection' is null.");
            }

            // Retrieve and delete the Disinfection record by Id
            var disinfection = await _context.Disinfection.FindAsync(id);
            if (disinfection != null)
            {
                _context.Disinfection.Remove(disinfection);
            }

            // Save changes to finalize deletion and redirect to Index view
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a Disinfection record with the given Id exists
        private bool DisinfectionExists(int id)
        {
          return (_context.Disinfection?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
