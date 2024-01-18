using System.ComponentModel.DataAnnotations;

namespace ShoesShelf.Models
{
    public class Rental
    {        
        public int ID { get; set; }

        [Display(Name = "Shoes")]
        public int ShoeID { get; set; }
        
        public Shoe Shoe { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; }
    }
}
