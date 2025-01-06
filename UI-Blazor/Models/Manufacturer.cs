using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.ComponentModel.DataAnnotations;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_Blazor.Models
{
    public class Manufacturer : IManufacturer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa producenta jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa nie może przekraczać 100 znaków.")]
        public string Name { get; set; }
    }
}
