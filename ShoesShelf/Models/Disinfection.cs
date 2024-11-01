using System.ComponentModel.DataAnnotations;

namespace ShoesShelf.Models
{
    // Represents a record of a disinfection process applied to a shoe, including the date of disinfection
    public class Disinfection
    {
        public int ID { get; set; }

        [Display(Name = "Shoes")]
        // Foreign key linking to the associated Shoe entity
        public int ShoeID { get; set; }

        // Navigation property for accessing the related Shoe details
        public Shoe Shoe { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Disinfection Date")]
        // Date when the disinfection procedure was performed, formatted as dd.MM.yyyy
        public DateTime DisinfectionDate { get; set; }
    }
}
