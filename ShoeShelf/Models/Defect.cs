using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeShelf.Models
{
    public enum Severity { Minor, Major, Critical }

    // Represents a defect associated with a shoe, including details like severity and description
    public class Defect
    {
        public int Id { get; set; }

        [Display(Name = "Shoes")]
        // Foreign key linking to the Shoe entity
        public int ShoeId { get; set; }

        // Navigation property to access the related Shoe
        public Shoe Shoe { get; set; }

        // Enum indicating the severity level of the defect (e.g., Minor, Major, Critical)
        public Severity Severity { get; set; }

        public string Description { get; set; }

        [NotMapped]
        // List of preset defect descriptions for use in UI dropdowns, not stored in the database
        public List<SelectListItem> Descriptions { get; } = new List<SelectListItem>
        {
            new() { Value = "Cracked sole", Text = "Cracked sole" },
            new() { Value = "Cracked stitching", Text = "Cracked stitching" },
            new() { Value = "Cracked upper material", Text = "Cracked upper material" },
            new() { Value = "Destruction of the surface", Text = "Destruction of the surface" },
            new() { Value = "Heel release", Text = "Heel release" },
            new() { Value = "Ungluing", Text = "Ungluing" },
        };
    }
}
