using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShelf.Data;
using ShoesShelf.Models;
using ShoesShelf.Models.ShoeViewModels;
using System.Collections.Concurrent;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Humanizer;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.Text;
using System.ComponentModel;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using X.PagedList;
using System.Composition;

namespace ShoesShelf.Controllers
{
    /// <summary>
    /// ReportsController provides various reports and general pages for the application.
    /// This includes Disinfection, Rental, and Substitution reports, as well as the 
    /// home index, privacy, and error handling views. The reports feature sorting 
    /// and pagination for enhanced data accessibility.
    /// </summary>
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes the ReportsController with an ILogger and ApplicationDbContext,
        /// supporting logging and database access for report data.
        /// </summary>
        /// <param name="logger">Logger for logging operations within the controller.</param>
        /// <param name="context">Database context for data access and manipulation.</param>
        public ReportsController(ILogger<ReportsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: DisinfectionReport
        /// <summary>
        /// Generates a paginated and sortable report of disinfections for shoes.
        /// Includes details on each shoe's brand, category, size, and disinfection date.
        /// Allows sorting by disinfection date, brand, Id, and inclusion date.
        /// </summary>
        /// <param name="pageNumber">Optional page number for pagination.</param>
        /// <param name="sortOrder">Sort criteria for the report (e.g., by date, brand).</param>
        /// <returns>Paginated view of disinfection reports, sorted as specified.</returns>
        public async Task<ActionResult> DisinfectionReport(int? pageNumber, string sortOrder)
        {
            IQueryable<Reports> data =
                (from s in _context.Shoe
                 join d in _context.Disinfection on s.Id equals d.ShoeId into disinfectionGroup
                 from d in disinfectionGroup.DefaultIfEmpty()
                 select new Reports()
                 {
                     Id = s.Id,
                     Brand = s.Brand,
                     Category = s.Category,
                     Size = s.Size,
                     InclusionDate = s.InclusionDate,
                     DisinfectionDate = (from d2 in _context.Disinfection
                                         where d2.ShoeId == s.Id
                                         orderby d2.DisinfectionDate descending
                                         select d2.DisinfectionDate).FirstOrDefault()
                 }).AsNoTracking().Distinct().OrderBy(x => x.DisinfectionDate);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["DisinfectionDateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "disinfectionDate_desc" : "";
            ViewData["BrandSortParm"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["IdSortParm"] = sortOrder == "Id" ? "id_desc" : "Id";
            ViewData["InclusionDateSortParm"] = sortOrder == "InclusionDate" ? "inclusionDate_desc" : "InclusionDate";

            data = sortOrder switch
            {
                "disinfectionDate_desc" => data.OrderByDescending(s => s.DisinfectionDate),
                "Brand" => data.OrderBy(s => s.Brand),
                "brand_desc" => data.OrderByDescending(s => s.Brand),
                "Id" => data.OrderBy(s => s.Id),
                "id_desc" => data.OrderByDescending(s => s.Id),
                "InclusionDate" => data.OrderBy(s => s.InclusionDate),
                "inclusionDate_desc" => data.OrderByDescending(s => s.InclusionDate),
                _ => data.OrderBy(s => s.DisinfectionDate),
            };

            int pageSize = 9;
            return View(await PaginatedList<Reports>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: RentalReport
        /// <summary>
        /// Generates a paginated and sortable report on rental frequency of shoes,
        /// including brand, category, size, and price. Sort options include count,
        /// brand, and price, enabling insights into popular rentals.
        /// </summary>
        /// <param name="pageNumber">Optional page number for pagination.</param>
        /// <param name="sortOrder">Sort criteria for the report (e.g., by count, brand).</param>
        /// <returns>Paginated view of rental frequency reports, sorted as specified.</returns>
        public async Task<ActionResult> RentalReport(int? pageNumber, string sortOrder)
        {
            IQueryable<Reports> data =
                from rental in _context.Rental
                join shoe in _context.Shoe on rental.ShoeId equals shoe.Id into shoeGroup
                from shoe in shoeGroup.DefaultIfEmpty()
                group new { rental, shoe } by new { shoe.Brand, shoe.Category, shoe.Size, shoe.Price } into g
                orderby g.Count() descending, g.Key.Price
                select new Reports()
                {
                    Brand = g.Key.Brand,
                    Category = g.Key.Category,
                    Size = g.Key.Size,
                    Price = g.Key.Price,
                    Count = g.Count()
                };

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CountSortParm"] = string.IsNullOrEmpty(sortOrder) ? "Count" : "";
            ViewData["BrandSortParm"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            data = sortOrder switch
            {
                "Count" => data.OrderBy(s => s.Count).ThenBy(s => s.Price),
                "Brand" => data.OrderBy(s => s.Brand),
                "brand_desc" => data.OrderByDescending(s => s.Brand),
                "Price" => data.OrderBy(s => s.Price),
                "price_desc" => data.OrderByDescending(s => s.Price),
                _ => data.OrderByDescending(s => s.Count).ThenByDescending(s => s.Price),
            };

            int pageSize = 9;
            return View(await PaginatedList<Reports>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: SubstitutionReport
        /// <summary>
        /// Generates a paginated and sortable report of shoes with severe defects 
        /// (Severity level 2) indicating need for substitution, including brand, 
        /// category, and size. Sort options include brand, size, and category.
        /// </summary>
        /// <param name="pageNumber">Optional page number for pagination.</param>
        /// <param name="sortOrder">Sort criteria for the report (e.g., by brand, size).</param>
        /// <returns>Paginated view of substitution reports, sorted as specified.</returns>
        public async Task<ActionResult> SubstitutionReport(int? pageNumber, string sortOrder)
        {
            IQueryable<Reports> data =
                from shoe in _context.Shoe
                join defect in _context.Defect on shoe.Id equals defect.ShoeId into shoeDefects
                from sd in shoeDefects.DefaultIfEmpty()
                where sd.Severity == (Severity)2
                group new { shoe.Id, shoe.Brand, shoe.Category, shoe.Size } by new { shoe.Brand, shoe.Category, shoe.Size } into g
                select new Reports()
                {
                    Brand = g.Key.Brand,
                    Category = g.Key.Category,
                    Size = g.Key.Size,
                    Count = g.Count()
                };

            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandSortParm"] = string.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["CountSortParm"] = sortOrder == "Count" ? "count_desc" : "Count";

            data = sortOrder switch
            {
                "brand_desc" => data.OrderByDescending(s => s.Brand),
                "Size" => data.OrderBy(s => s.Size),
                "size_desc" => data.OrderByDescending(s => s.Size),
                "Category" => data.OrderBy(s => s.Category),
                "category_desc" => data.OrderByDescending(s => s.Category),
                "Count" => data.OrderBy(s => s.Count),
                "count_desc" => data.OrderByDescending(s => s.Count),
                _ => data.OrderBy(s => s.Brand),
            };

            int pageSize = 9;
            return View(await PaginatedList<Reports>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Index
        /// <summary>
        /// Returns the main index page view.
        /// </summary>
        /// <returns>Main home view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        // GET: Privacy
        /// <summary>
        /// Returns the privacy policy view.
        /// </summary>
        /// <returns>Privacy policy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Error
        /// <summary>
        /// Displays the error view, with diagnostic information.
        /// Caches the response for 0 seconds to prevent storing sensitive error data.
        /// </summary>
        /// <returns>Error view with error information.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}