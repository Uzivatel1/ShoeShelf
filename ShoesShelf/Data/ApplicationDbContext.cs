using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesShelf.Models;

namespace ShoesShelf.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ShoesShelf.Models.Rental> Rental { get; set; }
        public DbSet<ShoesShelf.Models.Shoe> Shoe { get; set; }
        public DbSet<ShoesShelf.Models.Disinfection> Disinfection { get; set; }
        public DbSet<ShoesShelf.Models.Defect> Defect { get; set; }
    }
}