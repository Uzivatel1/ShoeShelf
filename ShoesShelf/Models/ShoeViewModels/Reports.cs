using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShoesShelf.Models.ShoeViewModels
{
    public class Reports
    {
        public int ID { get; set; }
        public string Brand { get; set; }

        public Category Category { get; set; }

        public double Size { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:# ### ### Kč}")]
        public double Price { get; set; }

        public int Count { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Inclusion Date")]
        public DateTime InclusionDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Disinfection Date")]
        public DateTime DisinfectionDate { get; set; }
    }
}
