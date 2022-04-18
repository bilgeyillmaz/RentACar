using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class GearType
    {
        [Key]
        public byte GearTypeID { get; set; }
        [Required(ErrorMessage = "Vites Tipi boş geçilemez!"), Display(Name = "Vites Tipi"), StringLength(20, MinimumLength = 3, ErrorMessage = "{0} {2}-{1} Karakter olmalı!")]
        public string GearTypeName { get; set; }
    }
}