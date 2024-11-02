using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoesShelf.Data;
using ShoesShelf.Models;

namespace ShoesShelf.Controllers
{
    /// <summary>
    /// RentalsController manages the CRUD operations for Rental records in the application,
    /// including creating, viewing, editing, and deleting rental entries associated with specific shoes.
    /// It integrates with ApplicationDbContext and uses dependency injection for data access,
    /// providing both detailed record views and paginated lists in the Index action.
    /// </summary>
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes the RentalsController with an ApplicationDbContext instance via dependency injection.
        /// </summary>
        /// <param name="context">Database context for accessing rental and shoe data.</param>
        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        /// <summary>
        /// Displays a paginated list of all rentals, with each rental including its associated shoe information.
        /// </summary>
        /// <param name="pageNumber">Optional page number to display in pagination.</param>
        /// <returns>Paginated view of rental entries.</returns>
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var applicationDbContext = _context.Rental.Include(r => r.Shoe);
            int pageSize = 8;
            return View(await PaginatedList<Rental>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Rentals/Details/5
        /// <summary>
        /// Retrieves details of a specific rental by ID, including associated shoe information.
        /// Returns NotFound if the rental is not found.
        /// </summary>
        /// <param name="id">Rental ID to retrieve.</param>
        /// <returns>Detailed view of the selected rental, or NotFound if rental does not exist.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Shoe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        /// <summary>
        /// Provides a form for creating a new rental record. Populates the Shoe selection dropdown.
        /// </summary>
        /// <returns>Create view with a form for adding a new rental.</returns>
        public IActionResult Create()
        {
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition");
            return View();
        }

        // POST: Rentals/Create
        /// <summary>
        /// Processes the creation of a new rental entry and saves it to the database.
        /// Returns to Index view upon successful creation; otherwise, redisplays the form.
        /// </summary>
        /// <param name="rental">Rental object with bound form data.</param>
        /// <returns>Redirects to Index if successful, or the Create view with validation errors if not.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, ShoeID, RentalDate")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", rental.ShoeID);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        /// <summary>
        /// Provides a form for editing an existing rental record.
        /// Returns NotFound if the rental does not exist.
        /// </summary>
        /// <param name="id">Rental ID to edit.</param>
        /// <returns>Edit view with populated rental data, or NotFound if rental is not found.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", rental.ShoeID);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        /// <summary>
        /// Saves the edited rental details to the database.
        /// Handles concurrency exceptions if the rental record was modified elsewhere.
        /// </summary>
        /// <param name="id">Rental ID being edited.</param>
        /// <param name="rental">Updated rental data to save.</param>
        /// <returns>Redirects to Index if successful; redisplays Edit view if there are validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, ShoeID, RentalDate")] Rental rental)
        {
            if (id != rental.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.ID))
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
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", rental.ShoeID);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        /// <summary>
        /// Displays a confirmation view for deleting a specific rental.
        /// Returns NotFound if the rental does not exist.
        /// </summary>
        /// <param name="id">Rental ID to delete.</param>
        /// <returns>Confirmation view for deletion, or NotFound if rental is not found.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Shoe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        /// <summary>
        /// Confirms deletion of a rental record. Redirects to Index upon successful deletion.
        /// Returns Problem if the Rental entity set is null.
        /// </summary>
        /// <param name="id">Rental ID to delete.</param>
        /// <returns>Redirects to Index if successful, or Problem if context is null.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rental' is null.");
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper Method
        /// <summary>
        /// Checks if a rental record exists in the database by ID.
        /// </summary>
        /// <param name="id">Rental ID to verify.</param>
        /// <returns>True if rental exists; otherwise, false.</returns>
        private bool RentalExists(int id)
        {
            return _context.Rental.Any(e => e.ID == id);
        }
    }
}
