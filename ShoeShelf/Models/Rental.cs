using System.ComponentModel.DataAnnotations;

namespace ShoeShelf.Models
{
    // Represents a record of a rental event for a shoe, including details like rental date
    public class Rental
    {        
        public int Id { get; set; }

        [Display(Name = "Shoes")]
        // Foreign key linking to the associated Shoe entity
        public int ShoeId { get; set; }

        // Navigation property to access details of the rented Shoe
        public Shoe Shoe { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Rental Date")]
        // Date when the shoe was rented, formatted as dd.MM.yyyy
        public DateTime RentalDate { get; set; }
    }
}
