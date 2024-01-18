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
    public class DefectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DefectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Defects
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "shoeID_desc" : "";
            ViewData["BrandSortParm"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["DefectSortParm"] = sortOrder == "Defect" ? "defect_desc" : "Defect";
            ViewData["SeveritySortParm"] = sortOrder == "Severity" ? "severity_desc" : "Severity";

            var defect = from d in _context.Defect.Include(r => r.Shoe) select d;

            switch (sortOrder)
            {
                case "shoeID_desc":
                    defect = defect.OrderByDescending(s => s.Shoe.ID);
                    break;
                case "Brand":
                    defect = defect.OrderBy(s => s.Shoe.Brand);
                    break;
                case "brand_desc":
                    defect = defect.OrderByDescending(s => s.Shoe.Brand);
                    break;
                case "Category":
                    defect = defect.OrderBy(s => s.Shoe.Category);
                    break;
                case "category_desc":
                    defect = defect.OrderByDescending(s => s.Shoe.Category);
                    break;
                case "Size":
                    defect = defect.OrderBy(s => s.Shoe.Size);
                    break;
                case "size_desc":
                    defect = defect.OrderByDescending(s => s.Shoe.Size);
                    break;
                case "Defect":
                    defect = defect.OrderBy(s => s.Description);
                    break;
                case "defect_desc":
                    defect = defect.OrderByDescending(s => s.Description);
                    break;
                case "Severity":
                    defect = defect.OrderBy(s => s.Severity);
                    break;
                case "severity_desc":
                    defect = defect.OrderByDescending(s => s.Severity);
                    break;
                default:
                    defect = defect.OrderBy(s => s.ShoeID);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Defect>.CreateAsync(defect.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Defects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Defect == null)
            {
                return NotFound();
            }

            var defect = await _context.Defect
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);
        }

        // GET: Defects/Create
        public IActionResult Create()
        {
            ViewBag.Descriptions = new List<SelectListItem> // https://stackoverflow.com/questions/34624034/select-tag-helper-in-asp-net-core-mvc
            {
                new() { Value = "Cracked sole", Text = "Cracked sole" },
                new() { Value = "Cracked stitching", Text = "Cracked stitching" },
                new() { Value = "Cracked upper material", Text = "Cracked upper material" },
                new() { Value = "Destruction of the surface", Text = "Destruction of the surface" },
                new() { Value = "Heel release", Text = "Heel release" },
                new() { Value = "Ungluing", Text = "Ungluing" },
            };
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition");
            return View();
        }

        // POST: Defects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ShoeID,Severity,Description")] Defect defect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(defect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", defect.ShoeID);
            return View(defect);
        }

        // GET: Defects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Defect == null)
            {
                return NotFound();
            }

            var defect = await _context.Defect.FindAsync(id);
            if (defect == null)
            {
                return NotFound();
            }
            ViewBag.Descriptions = new List<SelectListItem> // https://stackoverflow.com/questions/34624034/select-tag-helper-in-asp-net-core-mvc
            {
                new() { Value = "Cracked sole", Text = "Cracked sole" },
                new() { Value = "Cracked stitching", Text = "Cracked stitching" },
                new() { Value = "Cracked upper material", Text = "Cracked upper material" },
                new() { Value = "Destruction of the surface", Text = "Destruction of the surface" },
                new() { Value = "Heel release", Text = "Heel release" },
                new() { Value = "Ungluing", Text = "Ungluing" },
            };
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", defect.ShoeID);
            return View(defect);
        }

        // POST: Defects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ShoeID,Severity,Description")] Defect defect)
        {
            if (id != defect.ID)
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
                    if (!DefectExists(defect.ID))
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
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", defect.ShoeID);
            return View(defect);
        }

        // GET: Defects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Defect == null)
            {
                return NotFound();
            }

            var defect = await _context.Defect
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);
        }

        // POST: Defects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Defect == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Defect'  is null.");
            }
            var defect = await _context.Defect.FindAsync(id);
            if (defect != null)
            {
                _context.Defect.Remove(defect);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefectExists(int id)
        {
          return _context.Defect.Any(e => e.ID == id);
        }
    }
}
