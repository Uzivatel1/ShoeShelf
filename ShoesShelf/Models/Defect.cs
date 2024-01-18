using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesShelf.Models
{
    public enum Severity { Minor, Major, Critical }

    public class Defect
    {
        public int ID { get; set; }

        [Display(Name = "Shoes")]
        public int ShoeID { get; set; }

        public Shoe Shoe { get; set; }

        public Severity Severity { get; set; }

        public string Description { get; set; }

        [NotMapped]
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
