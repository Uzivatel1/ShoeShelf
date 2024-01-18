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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ActionResult> DisinfectionReport(int? pageNumber, string sortOrder)
        {
            //SELECT DISTINCT S.ID, Brand, Category, Size, InclusionDate,
            //(SELECT TOP 1 DisinfectionDate FROM Disinfection AS D WHERE D.ShoeID = S.ID ORDER BY DisinfectionDate DESC) AS DisinfectionDate
            //FROM Shoe AS S
            //LEFT JOIN Disinfection ON Disinfection.ShoeID = S.ID
            //ORDER BY DisinfectionDate;
            IQueryable<Reports> data =
                (from s in _context.Shoe
                join d in _context.Disinfection on s.ID equals d.ShoeID into disinfectionGroup
                from d in disinfectionGroup.DefaultIfEmpty()
                select new Reports()
                {
                    ID = s.ID,
                    Brand = s.Brand,
                    Category = s.Category,
                    Size = s.Size,
                    InclusionDate = s.InclusionDate,
                    DisinfectionDate = (from d2 in _context.Disinfection
                                        where d2.ShoeID == s.ID
                                        orderby d2.DisinfectionDate descending
                                        select d2.DisinfectionDate).FirstOrDefault()
                }).Distinct().OrderBy(x => x.DisinfectionDate);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["DisinfectionDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "disinfectionDate_desc" : "";
            ViewData["BrandSortParm"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["IDSortParm"] = sortOrder == "ID" ? "id_desc" : "ID";
            ViewData["InclusionDateSortParm"] = sortOrder == "InclusionDate" ? "inclusionDate_desc" : "InclusionDate";
            switch (sortOrder)
            {
                case "disinfectionDate_desc":
                    data = data.OrderByDescending(s => s.DisinfectionDate);
                    break;
                case "Brand":
                    data = data.OrderBy(s => s.Brand);
                    break;
                case "brand_desc":
                    data = data.OrderByDescending(s => s.Brand);
                    break;
                case "ID":
                    data = data.OrderBy(s => s.ID);
                    break;
                case "id_desc":
                    data = data.OrderByDescending(s => s.ID);
                    break;
                case "InclusionDate":
                    data = data.OrderBy(s => s.InclusionDate);
                    break;
                case "inclusionDate_desc":
                    data = data.OrderByDescending(s => s.InclusionDate);
                    break;
                default:
                    data = data.OrderBy(s => s.DisinfectionDate);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Reports>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<ActionResult> RentalReport(int? pageNumber, string sortOrder)
        {
            //SELECT Brand, Category, Size, Price, COUNT(*) AS Count
            //FROM Rental JOIN Shoe ON Rental.ShoeID = Shoe.ID
            //GROUP BY Brand, Category, Size, Price
            //ORDER BY Count DESC, Price;
            IQueryable<Reports> data =
                from rental in _context.Rental
                join shoe in _context.Shoe on rental.ShoeID equals shoe.ID into shoeGroup
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
            ViewData["CountSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Count" : "";
            ViewData["BrandSortParm"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            switch (sortOrder)
            {
                case "Count":
                    data = data.OrderBy(s => s.Count).ThenBy(s => s.Price);
                    break;
                case "Brand":
                    data = data.OrderBy(s => s.Brand);
                    break;
                case "brand_desc":
                    data = data.OrderByDescending(s => s.Brand);
                    break;
                case "Price":
                    data = data.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    data = data.OrderByDescending(s => s.Price);
                    break;
                default:
                    data = data.OrderByDescending(s => s.Count).ThenByDescending(s => s.Price);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Reports>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<ActionResult> SubstitutionReport(int? pageNumber, string sortOrder)
        {
            //SELECT Brand, Category, Size, COUNT(*) AS Count FROM Shoe
            //JOIN Defect ON Defect.ShoeID = Shoe.ID WHERE Severity = '2'
            //GROUP BY Brand, Category, Size
            IQueryable<Reports> data =
                from shoe in _context.Shoe
                join defect in _context.Defect on shoe.ID equals defect.ShoeID into shoeDefects
                from sd in shoeDefects.DefaultIfEmpty()
                where sd.Severity == (Severity)2
                group new { shoe.ID, shoe.Brand, shoe.Category, shoe.Size } by new { shoe.Brand, shoe.Category, shoe.Size } into g
                select new Reports()
                {
                    Brand = g.Key.Brand,
                    Category = g.Key.Category,
                    Size = g.Key.Size,
                    Count = g.Count()
                };

            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["CountSortParm"] = sortOrder == "Count" ? "count_desc" : "Count";
            switch (sortOrder)
            {
                case "brand_desc":
                    data = data.OrderByDescending(s => s.Brand);
                    break;
                case "Size":
                    data = data.OrderBy(s => s.Size);
                    break;
                case "size_desc":
                    data = data.OrderByDescending(s => s.Size);
                    break;
                case "Category":
                    data = data.OrderBy(s => s.Category);
                    break;
                case "category_desc":
                    data = data.OrderByDescending(s => s.Category);
                    break;
                case "Count":
                    data = data.OrderBy(s => s.Count);
                    break;
                case "count_desc":
                    data = data.OrderByDescending(s => s.Count);
                    break;
                default:
                    data = data.OrderBy(s => s.Brand);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Reports>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}