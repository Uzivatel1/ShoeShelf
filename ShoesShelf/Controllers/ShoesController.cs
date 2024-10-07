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
    public class ShoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoesController(ApplicationDbContext context)
        {
            _context = context;
        }        
        
        // GET: Shoes
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilterBrand,
            double? currentFilterSize,
            string searchBrand,
            double? searchSize,
            bool? searchRented,
            bool? currentFilterRented,
            bool? searchUnrented,
            bool? currentFilterUnrented,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if ((searchBrand != null) || (searchSize != null) || (searchRented != null) || (searchUnrented != null))
            {
                pageNumber = 1;
            }

            else
            {
                searchBrand = currentFilterBrand;
                searchSize = currentFilterSize;
                searchRented = currentFilterRented;
                searchUnrented = currentFilterUnrented;
            }

            ViewData["CurrentFilterBrand"] = searchBrand;
            ViewData["CurrentFilterSize"] = searchSize;
            ViewData["CurrentFilterRented"] = searchRented;
            ViewData["CurrentFilterUnrented"] = searchUnrented;
            
            var shoes = from s in _context.Shoe
                           select s;

            if (!String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchSize.ToString()) && !String.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Size.Equals(searchSize) && s.Rented.Equals(searchRented));
            }

            else if (!String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchSize.ToString()) && !String.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Size.Equals(searchSize) && !s.Rented.Equals(searchUnrented));
            }

            else if (!String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchSize.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Size.Equals(searchSize));
            }

            else if (!String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && s.Rented.Equals(searchRented));
            }

            else if (!String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) && !s.Rented.Equals(searchUnrented));
            }

            if (!String.IsNullOrEmpty(searchSize.ToString()) && !String.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Size.Equals(searchSize) && s.Rented.Equals(searchRented));
            }

            else if (!String.IsNullOrEmpty(searchSize.ToString()) && !String.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => s.Size.Equals(searchSize) && !s.Rented.Equals(searchUnrented));
            }

            else if (!String.IsNullOrEmpty(searchBrand) || !String.IsNullOrEmpty(searchSize.ToString()))
            {
                shoes = shoes.Where(s => s.Brand.Contains(searchBrand) || s.Size.Equals(searchSize));
            }

            else if (!String.IsNullOrEmpty(searchRented.ToString()))
            {
                shoes = shoes.Where(s => s.Rented.Equals(searchRented));
            }

            else if (!String.IsNullOrEmpty(searchUnrented.ToString()))
            {
                shoes = shoes.Where(s => !s.Rented.Equals(searchUnrented));
            }

            switch (sortOrder)
            {
                case "brand_desc":
                    shoes = shoes.OrderByDescending(s => s.Brand);
                    break;
                case "Size":
                    shoes = shoes.OrderBy(s => s.Size);
                    break;
                case "size_desc":
                    shoes = shoes.OrderByDescending(s => s.Size);
                    break;
                case "Price":
                    shoes = shoes.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    shoes = shoes.OrderByDescending(s => s.Price);
                    break;
                case "Date":
                    shoes = shoes.OrderBy(s => s.InclusionDate);
                    break;
                case "date_desc":
                    shoes = shoes.OrderByDescending(s => s.InclusionDate);
                    break;
                default:
                    shoes = shoes.OrderBy(s => s.Brand);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Shoe>.CreateAsync(shoes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Shoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shoe == null)
            {
                return NotFound();
            }

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
        public IActionResult Create()
        {
            ViewBag.Sizes = new List<SelectListItem> // https://stackoverflow.com/questions/34624034/select-tag-helper-in-asp-net-core-mvc
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

        // POST: Shoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Brand,Category,Size,Price,InclusionDate,Rented")] Shoe shoe)
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

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Brand,Category,Size,Price,InclusionDate,Rented")] Shoe shoe)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shoe == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Shoe'  is null.");
            }
            var shoe = await _context.Shoe.FindAsync(id);
            if (shoe != null)
            {
                _context.Shoe.Remove(shoe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoeExists(int id)
        {
          return (_context.Shoe?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
