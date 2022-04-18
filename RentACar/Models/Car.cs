using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Marka ID")]
        public byte BrandID { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Renk ID")]

        public byte CarColorID { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Araba Modeli"), StringLength(50, MinimumLength = 3, ErrorMessage = "{0} {2}-{1} Karakter olmalı!")]
        public string CarModel { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Yakıt Tipi ID")]
        public byte FuelTypeID { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Vites Tipi ID")]
        public byte GearTypeID { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Koltuk Sayısı")]
        public byte SeatNumber { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Kasa Tipi ID")]
        public byte BodyTypeID { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Bagaj Hacmi")]
        public short LuggageVolume { get; set; }
        [Required(ErrorMessage = "{0} Boş geçilemez!"), Display(Name = "Model Yılı")]
        public short ModelYear { get; set; } 

        public Brand Brand { get; set; }
        public CarColor CarColor { get; set; }
        public FuelType FuelType { get; set; }
        public GearType GearType { get; set; }
        public BodyType BodyType { get; set; }

    }
}
