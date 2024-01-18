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
    public class DisinfectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisinfectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Disinfections
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var applicationDbContext = _context.Disinfection.Include(r => r.Shoe);
            int pageSize = 8;
            return View(await PaginatedList<Disinfection>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Disinfections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Disinfection == null)
            {
                return NotFound();
            }

            var disinfection = await _context.Disinfection
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (disinfection == null)
            {
                return NotFound();
            }

            return View(disinfection);
        }

        // GET: Disinfections/Create
        public IActionResult Create()
        {
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition");
            return View();
        }

        // POST: Disinfections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ShoeID,DisinfectionDate")] Disinfection disinfection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disinfection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", disinfection.ShoeID);
            return View(disinfection);
        }

        // GET: Disinfections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Disinfection == null)
            {
                return NotFound();
            }

            var disinfection = await _context.Disinfection.FindAsync(id);
            if (disinfection == null)
            {
                return NotFound();
            }
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", disinfection.ShoeID);
            return View(disinfection);
        }

        // POST: Disinfections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ShoeID,DisinfectionDate")] Disinfection disinfection)
        {
            if (id != disinfection.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disinfection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisinfectionExists(disinfection.ID))
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
            ViewData["ShoeID"] = new SelectList(_context.Shoe, "ID", "FullDefinition", disinfection.ShoeID);
            return View(disinfection);
        }

        // GET: Disinfections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disinfection == null)
            {
                return NotFound();
            }

            var disinfection = await _context.Disinfection
                .Include(d => d.Shoe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (disinfection == null)
            {
                return NotFound();
            }

            return View(disinfection);
        }

        // POST: Disinfections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disinfection == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Disinfection'  is null.");
            }
            var disinfection = await _context.Disinfection.FindAsync(id);
            if (disinfection != null)
            {
                _context.Disinfection.Remove(disinfection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisinfectionExists(int id)
        {
          return (_context.Disinfection?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
