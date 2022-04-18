using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class FuelType
    {
        [Key]
        public byte FuelTypeID { get; set; }
        [Required(ErrorMessage = "Yakıt Tipi boş geçilemez!"), Display(Name = "Yakıt Türü"), StringLength(20, MinimumLength = 3, ErrorMessage = "{0} {2}-{1} Karakter olmalı!")]
        public string FuelTypeName { get; set; }
    }
}