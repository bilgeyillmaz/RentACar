using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class BodyType
    {
        [Key]
        public byte BodyTypeID { get; set; }
        [Required(ErrorMessage = "Kasa Tipi boş geçilemez!"), Display(Name = "Kasa Tipi"), StringLength(20, MinimumLength = 3, ErrorMessage = "{0} {2}-{1} Karakter olmalı!")]
        public string BodyTypeName { get; set; }
    }
}