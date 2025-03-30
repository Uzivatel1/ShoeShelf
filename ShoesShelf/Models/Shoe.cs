using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ShoesShelf.Models
{
    // Represents categories of shoes by gender
    public enum Category { Male, Female }

    // Represents a shoe model in the system with properties for brand, size, price and additional attributes.
    public class Shoe
    {
        [Display(Name = "Shoes No")]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public Category Category { get; set; }

        public double Size { get; set; }

        [NotMapped]
        // List of possible shoe sizes used for display purposes only, not stored in the database
        public List<SelectListItem> Sizes { get; } = new List<SelectListItem>
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

        [DisplayFormat(DataFormatString = "{0:N0} Kč", ApplyFormatInEditMode = false)]
        // Stores the price of the shoe in Czech Koruna (Kč) currency format
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Inclusion Date")]
        // Date when the shoe was added to inventory, formatted as dd.MM.yyyy
        public DateTime InclusionDate { get; set; }

        // List of defects associated with the shoe, if any
        public ICollection<Defect> Defects { get; set;}

        // List of rentals associated with the shoe
        public ICollection<Rental> Rentals { get; set; }

        // List of disinfections applied to the shoe
        public ICollection<Disinfection> Disinfections { get; set; }

        public bool Rented { get; set; }

        [Display(Name = "Full Definition")]
        public string FullDefinition
        {
            get
            {
                return Id + " " + Brand + " " + Category + " " + Size;
            }
        }
    }
}
