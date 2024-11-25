using System.ComponentModel.DataAnnotations;
using CORE;
using INTERFACES;

namespace UI_Blazor.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa nie może przekraczać 100 znaków.")]
        public string Name { get; set; }

        [Required]
        public IManufacturer Manufacturer { get; set; }

        [Required]
        public KeyboardType Type { get; set; }
    }

}
