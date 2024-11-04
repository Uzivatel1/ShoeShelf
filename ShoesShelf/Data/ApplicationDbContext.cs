using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesShelf.Models;

namespace ShoesShelf.Data
{
    /// <summary>
    /// ApplicationDbContext is the main database context for this application, inheriting from IdentityDbContext to integrate 
    /// ASP.NET Core Identity for authentication and authorization. It provides database access for custom entities and Identity
    /// tables, enabling seamless object-relational mapping and data persistence.
    ///
    /// This context defines the following DbSets:
    /// - Shoe: Maps to the Shoe table, which holds comprehensive information about each shoe item, including brand, size, 
    ///   category, and additional inventory-related properties.
    /// - Rental: Represents shoe rentals, storing details such as shoe IDs, rental dates, and associated rental data.
    /// - Disinfection: Tracks disinfection records, associating shoes with disinfection events and timestamps, which is crucial 
    ///   for managing health and safety standards within the rental lifecycle.
    /// - Defect: Records shoe defects, storing severity levels and descriptions to facilitate maintenance and ensure quality 
    ///   control.
    ///
    /// This context also provides IdentityDbContext functionality, handling application-specific user management and secure access 
    /// through Identity's user and role features.
    ///
    /// The constructor accepts DbContextOptions to allow configuration settings to be injected, enhancing flexibility across 
    /// environments (development, production, etc.).
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ShoesShelf.Models.Shoe> Shoe { get; set; }
        public DbSet<ShoesShelf.Models.Rental> Rental { get; set; }        
        public DbSet<ShoesShelf.Models.Disinfection> Disinfection { get; set; }
        public DbSet<ShoesShelf.Models.Defect> Defect { get; set; }
    }
}