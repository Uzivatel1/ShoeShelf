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

namespace ShoesShelf.Controllers
{
    /// <summary>
    /// DefectsController handles the CRUD operations and data management for shoe defects,
    /// allowing users to view, create, edit, delete, and sort defects linked to specific shoes.
    /// Includes pagination and sorting functionality for efficient browsing of defect records.
    /// </summary>
    [Authorize(Roles = UserRoles.Admin)]
    public class DefectsController : Controller
    {
        // Dependency Injection: Injects ApplicationDbContext to interact with the database
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the database context via dependency injection
        public DefectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Defects - lists all defects with sorting and pagination options
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = string.IsNullOrEmpty(sortOrder) ? "shoeId_desc" : "";
            ViewData["BrandSortParm"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["DefectSortParm"] = sortOrder == "Defect" ? "defect_desc" : "Defect";
            ViewData["SeveritySortParm"] = sortOrder == "Severity" ? "severity_desc" : "Severity";

            // Retrieve all defects, including related Shoe data
            var defect = from d in _context.Defect.Include(r => r.Shoe) select d;

            // Sorting logic based on the selected sort order
            defect = sortOrder switch
            {
                "shoeId_desc" => defect.OrderByDescending(s => s.Shoe.Id),
                "Brand" => defect.OrderBy(s => s.Shoe.Brand),
                "brand_desc" => defect.OrderByDescending(s => s.Shoe.Brand),
                "Category" => defect.OrderBy(s => s.Shoe.Category),
                "category_desc" => defect.OrderByDescending(s => s.Shoe.Category),
                "Size" => defect.OrderBy(s => s.Shoe.Size),
                "size_desc" => defect.OrderByDescending(s => s.Shoe.Size),
                "Defect" => defect.OrderBy(s => s.Description),
                "defect_desc" => defect.OrderByDescending(s => s.Description),
                "Severity" => defect.OrderBy(s => s.Severity),
                "severity_desc" => defect.OrderByDescending(s => s.Severity),
                _ => defect.OrderBy(s => s.ShoeId),
            };

            // Paginate the defects list with a defined page size
            int pageSize = 7;
            return View(await PaginatedList<Defect>.CreateAsync(defect.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Defects/Details/5 - Shows details for a specific defect
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Defect == null)
            {
                return NotFound();
            }

            // Retrieve defect by Id and include associated Shoe data
            var defect = await _context.Defect
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);
        }

        // GET: Defects/Create - Renders form to create a new defect
        public IActionResult Create()
        {
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition");
            ViewBag.Descriptions = new Defect().Descriptions;            
            return View();
        }

        // POST: Defects/Create - Saves a new defect to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, ShoeId, Severity, Description")] Defect defect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(defect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition", defect.ShoeId);
            return View(defect);
        }

        // GET: Defects/Edit/5 - Renders form to edit an existing defect
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Defect == null)
            {
                return NotFound();
            }

            // Load defect data by Id for editing
            var defect = await _context.Defect.FindAsync(id);
            if (defect == null)
            {
                return NotFound();
            }
            ViewBag.Descriptions = new Defect().Descriptions;
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition", defect.ShoeId);
            return View(defect);
        }

        // POST: Defects/Edit/5 - Updates an existing defect in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ShoeId, Severity, Description")] Defect defect)
        {
            if (id != defect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefectExists(defect.Id))
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
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "Id", "FullDefinition", defect.ShoeId);
            return View(defect);
        }

        // GET: Defects/Delete/5 - Displays confirmation page to delete a defect
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Defect == null)
            {
                return NotFound();
            }

            // Retrieve defect by Id to confirm deletion
            var defect = await _context.Defect
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);
        }

        // POST: Defects/Delete/5 - Deletes the defect from the database after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Defect == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Defect' is null.");
            }
            var defect = await _context.Defect.FindAsync(id);
            if (defect != null)
            {
                _context.Defect.Remove(defect);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Utility method to check if a defect with a given Id exists
        private bool DefectExists(int id)
        {
          return _context.Defect.Any(e => e.Id == id);
        }
    }
}