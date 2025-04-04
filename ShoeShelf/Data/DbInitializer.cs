using Microsoft.EntityFrameworkCore;
using ShoeShelf.Models;
using System.Diagnostics;

namespace ShoeShelf.Data
{
    /// <summary>
    /// The <c>DbInitializer</c> class is responsible for seeding the database with initial test data 
    /// during application startup. It checks if key entities, such as <c>Shoe</c>, already exist 
    /// in the database to avoid duplication. If no data is found, it populates the database with 
    /// a variety of shoes, defects, rentals, and disinfection records.
    ///
    /// This seeding logic is useful for development and testing, providing a predefined dataset
    /// that represents a sample inventory of shoes, including brands, categories, sizes, prices, 
    /// and availability. Additionally, it simulates defect history, rental records, and disinfection 
    /// logs with randomized dates and attributes to mimic real-world scenarios.
    ///
    /// Calls <c>DbInitializer.Initialize(context)</c> to seed the database with initial data if empty. 
    /// The data includes:
    /// - 40 pairs of shoes from different brands, categories, sizes, and prices
    /// - Randomly generated defects, rentals, and disinfection records for 90% of the shoes
    /// - ID reseeding to ensure IDs start from 1 for each seeding cycle (if required)
    ///
    /// Note: This class assumes that the database schema has been created and that the tables 
    /// for <c>Shoe</c>, <c>Defect</c>, <c>Rental</c>, and <c>Disinfection</c> entities exist.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any shoes.
            if (context.Shoe.Any())
            {
                return; // DB has been seeded
            }

            // Reset the shoe IDs after a database reset
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Shoe', RESEED, 1)");

            var random = new Random();
            var shoes = new List<Shoe>();
            var brands = new[] { "BRUNSWICK", "900 GLOBAL", "DEXTER" };
            var maleSizes = Enumerable.Range(40, 6).ToArray(); // Sizes 40 to 45
            var femaleSizes = Enumerable.Range(37, 6).ToArray(); // Sizes 37 to 42
            var categories = new[] { Category.Male, Category.Female };

            for (int i = 0; i < 40; i++)
            {
                var brand = brands[random.Next(brands.Length)];
                var category = categories[random.Next(categories.Length)];
                var size = category == Category.Male ? maleSizes[random.Next(maleSizes.Length)] : femaleSizes[random.Next(femaleSizes.Length)];

                decimal price = brand switch
                {
                    "BRUNSWICK" => category == Category.Male ? 2690M : 1990M,
                    "900 GLOBAL" => category == Category.Male ? 2390M : 1790M,
                    "DEXTER" => category == Category.Male ? 2890M : 2290M,
                    _ => 0M // default, although all cases are covered
                };

                var randomMonth = random.Next(1, 13);
                var randomDay = random.Next(1, DateTime.DaysInMonth(DateTime.Now.Year - 1, randomMonth) + 1);
                var inclusionDate = new DateTime(DateTime.Now.Year - 1, randomMonth, randomDay);

                bool rented = random.NextDouble() > 0.5;

                var shoe = new Shoe
                {
                    Brand = brand,
                    Category = category,
                    Size = size,
                    Price = price,
                    InclusionDate = inclusionDate,
                    Rented = rented
                };

                shoes.Add(shoe);
            }

            // Define possible defect severities and descriptions
            var severities = new[] { Severity.Minor, Severity.Major, Severity.Critical };
            var descriptions = new[]
            {
                "Cracked sole",
                "Cracked stitching",
                "Cracked upper material",
                "Destruction of the surface",
                "Heel release",
                "Ungluing"
            };

            var defects = new List<Defect>();
            var rentals = new List<Rental>();
            var disinfections = new List<Disinfection>();

            foreach (var shoe in shoes)
            {
                // Assign defects to 50% of the shoes
                if (random.NextDouble() < 0.5)
                {
                    var defectCount = random.Next(1, 4); // 1 to 3 defects per shoe
                    for (int j = 0; j < defectCount; j++)
                    {
                        defects.Add(new Defect
                        {
                            Shoe = shoe,
                            Severity = severities[random.Next(severities.Length)],
                            Description = descriptions[random.Next(descriptions.Length)]
                        });
                    }
                }

                // Assign random rental dates in the last year to 90% of shoes
                if (random.NextDouble() < 0.9)
                {
                    var rentalCount = random.Next(1, 4); // 1 to 3 rentals per shoe
                    for (int k = 0; k < rentalCount; k++)
                    {
                        var rentalMonth = random.Next(1, 13);
                        var rentalDay = random.Next(1, DateTime.DaysInMonth(DateTime.Now.Year - 1, rentalMonth) + 1);
                        rentals.Add(new Rental
                        {
                            Shoe = shoe,
                            RentalDate = new DateTime(DateTime.Now.Year - 1, rentalMonth, rentalDay)
                        });
                    }
                }

                // Assign random disinfection dates in the last year to 90% of shoes
                if (random.NextDouble() < 0.9)
                {
                    var disinfectionCount = random.Next(1, 4); // 1 to 3 disinfections per shoe
                    for (int l = 0; l < disinfectionCount; l++)
                    {
                        var disinfectionMonth = random.Next(1, 13);
                        var disinfectionDay = random.Next(1, DateTime.DaysInMonth(DateTime.Now.Year - 1, disinfectionMonth) + 1);
                        disinfections.Add(new Disinfection
                        {
                            Shoe = shoe,
                            DisinfectionDate = new DateTime(DateTime.Now.Year - 1, disinfectionMonth, disinfectionDay)
                        });
                    }
                }
            }

            // Save all the generated shoes, defects, rentals, and disinfections
            context.AddRange(shoes);
            context.AddRange(defects);
            context.AddRange(rentals);
            context.AddRange(disinfections);
            context.SaveChanges();
        }
    }
}
