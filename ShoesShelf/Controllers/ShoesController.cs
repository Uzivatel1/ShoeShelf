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
    // Controller for managing Shoe entities, including actions for viewing, creating, editing, and deleting shoes
    [Authorize(Roles = UserRoles.Admin)]
    public class ShoesController : Controller
    {
        // Dependency Injection: Injects ApplicationDbContext to interact with the database
        private readonly ApplicationDbContext _context;

        // Initializes the controller with the database context via dependency injection
        public ShoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shoes
        // Displays a paginated list of shoes with filtering and sorting options
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            int currentFilterId,
            string currentFilterBrand,
            double? currentFilterSize,
            bool? currentFilterRented,
            bool? currentFilterUnrented,
            string searchBrand,
            double? searchSize,
            bool? searchRented,            
            bool? searchUnrented,            
            int? pageNumber)
        {
            // Set current sort options for display in the view
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["BrandSortParm"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            // Reset page number if new search criteria are applied
            if ((searchBrand != null) || (searchSize != null) || (searchRented != null) || (searchUnrented != null))
            {
                pageNumber = 1;
            }

            else
            {
                // Maintain current filters if no new search criteria are applied
                searchBrand = currentFilterBrand;
                searchSize = currentFilterSize;
                searchRented = currentFilterRented;
                searchUnrented = currentFilterUnrented;
            }

            // Store the current filter criteria for use in the view
            ViewData["CurrentFilterBrand"] = searchBrand;
            ViewData["CurrentFilterSize"] = searchSize;
            ViewData["CurrentFilterRented"] = searchRented;
            ViewData["CurrentFilterUnrented"] = searchUnrented;

            // Initialize the query for shoes
            var shoes = from s in _context.Shoe
                           select s;

            // Apply filtering based on search criteria

            // Filters on brand, size, and rented/unrented status
            if (!string.IsNullOrEmpty(searchBrand) && !string.IsNullOrEmpty(searchSize.ToString()) && !string.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Size.Equals(searchSize) && s.Rented.Equals(searchRented));
            }

            else if (!string.IsNullOrEmpty(searchBrand) && !string.IsNullOrEmpty(searchSize.ToString()) && !string.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Size.Equals(searchSize) && !s.Rented.Equals(searchUnrented));
            }

            else if (!string.IsNullOrEmpty(searchBrand) && !string.IsNullOrEmpty(searchSize.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Size.Equals(searchSize));
            }

            else if (!string.IsNullOrEmpty(searchBrand) && !string.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Rented.Equals(searchRented));
            }

            else if (!string.IsNullOrEmpty(searchBrand) && !string.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && !s.Rented.Equals(searchUnrented));
            }

            if (!string.IsNullOrEmpty(searchSize.ToString()) && !string.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Size.Equals(searchSize) && s.Rented.Equals(searchRented));
            }

            else if (!string.IsNullOrEmpty(searchSize.ToString()) && !string.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => s.Size.Equals(searchSize) && !s.Rented.Equals(searchUnrented));
            }

            else if (!string.IsNullOrEmpty(searchBrand) || !string.IsNullOrEmpty(searchSize.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) || s.Size.Equals(searchSize));
            }

            else if (!string.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Rented.Equals(searchRented));
            }

            else if (!string.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => !s.Rented.Equals(searchUnrented));
            }

            // Apply sorting based on selected sort order
            shoes = sortOrder switch
            {
                "id_desc" => shoes.OrderByDescending(s => s.ID),
                "Brand" => shoes.OrderBy(s => s.Brand),
                "brand_desc" => shoes.OrderByDescending(s => s.Brand),
                "Size" => shoes.OrderBy(s => s.Size),
                "size_desc" => shoes.OrderByDescending(s => s.Size),
                "Price" => shoes.OrderBy(s => s.Price),
                "price_desc" => shoes.OrderByDescending(s => s.Price),
                "Date" => shoes.OrderBy(s => s.InclusionDate),
                "date_desc" => shoes.OrderByDescending(s => s.InclusionDate),
                _ => shoes.OrderBy(s => s.ID),
            };
            int pageSize = 6;
            // Return the paginated list of shoes to the view
            return View(await PaginatedList<Shoe>.CreateAsync(shoes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Shoes/Details/5
        // Retrieves and displays details for a specific shoe, including rentals, disinfections and defects
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            // Return 404 if no ID is provided or if the Shoe entity is not in the context
            if (id == null || _context.Shoe == null)
            {
                return NotFound();
            }

            // Fetch the shoe by ID, including related collections for display in the view
            var shoe = await _context.Shoe
                .Include(r => r.Rentals)
                .Include(i => i.Disinfections)
                .Include(e => e.Defects)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (shoe == null)
            {
                return NotFound();
            }

            return View(shoe);
        }

        // GET: Shoes/Create
        // Returns a view for creating a new shoe, including a list of available sizes
        public IActionResult Create()
        {
            ViewBag.Sizes = new List<SelectListItem>
            {
                new() { Value = "37", Text = "37" },
                new() { Value = "38", Text = "38" },
                new() { Value = "39", Text = "39" },
                new() { Value = "40", Text = "40" },
                new() { Value = "41", Text = "41" },
                new() { Value = "42", Text = "42" },
                new() { Value = "43", Text = "43" },
                new() { Value = "44", Text = "44" },
                new() { Value = "45", Text = "45" },
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Shoes/Create
        // Adds a new shoe to the database after validating the model state
        public async Task<IActionResult> Create([Bind("ID, Brand, Category, Size, Price, InclusionDate, Rented")] Shoe shoe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoe);
        }

        // GET: Shoes/Edit/5
        // Returns a view for editing an existing shoe
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shoe == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoe.FindAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Shoes/Edit/5
        // Updates an existing shoe record after validation, with error handling for concurrency issues
        public async Task<IActionResult> Edit(int id, [Bind("ID, Brand, Category, Size, Price, InclusionDate, Rented")] Shoe shoe)
        {
            if (id != shoe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoeExists(shoe.ID))
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
            return View(shoe);
        }

        // GET: Shoes/Delete/5
        // Retrieves and displays the shoe to be deleted
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shoe == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoe
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shoe == null)
            {
                return NotFound();
            }

            return View(shoe);
        }

        // POST: Shoes/Delete/5
        // Deletes the shoe after confirming the action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shoe == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Shoe' is null.");
            }
            var shoe = await _context.Shoe.FindAsync(id);
            if (shoe != null)
            {
                _context.Shoe.Remove(shoe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Checks if a shoe exists by ID
        private bool ShoeExists(int id)
        {
          return (_context.Shoe?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
