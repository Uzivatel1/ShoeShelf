using System.ComponentModel.DataAnnotations;

namespace ShoesShelf.Models
{
    public class Disinfection
    {
        public int ID { get; set; }

        [Display(Name = "Shoes")]
        public int ShoeID { get; set; }

        public Shoe Shoe { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Disinfection Date")]   
        public DateTime DisinfectionDate { get; set; }
    }
}
