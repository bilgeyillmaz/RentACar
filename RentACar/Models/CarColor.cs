using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class CarColor
    {
        [Key]
        public byte CarColorID { get; set; }
        [Required(ErrorMessage = "Renk Adı boş geçilemez!"), Display(Name = "Renk"), StringLength(20, MinimumLength = 3, ErrorMessage = "{0} {2}-{1} Karakter olmalı!")]
        public string CarColorName { get; set; }    

    }
}