using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ShoesShelf.Models
{
    public enum Category { Male, Female }

    public class Shoe
    {
        [Display(Name = "Shoes No")]
        public int ID { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public Category Category { get; set; }

        public double Size { get; set; }

        [NotMapped]
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

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0} Kč", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Inclusion Date")]
        public DateTime InclusionDate { get; set; }

        public ICollection<Defect> Defects { get; set;}

        public ICollection<Rental> Rentals { get; set; }

        public ICollection<Disinfection> Disinfections { get; set; }

        public bool Rented { get; set; }
    }
}
