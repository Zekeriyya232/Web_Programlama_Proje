using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje1.Entity
{
    [Table("Kullanici")]            //veritabanı tablo ismi
    public class KullaniciDB
    {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string kullaniciAdi{ get; set; }
        [Required,StringLength(20)]
        public string KullaniciSoyadi { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName ="Date")]
        public DateTime kullaniciDogum { get; set; }

        [Required(ErrorMessage = "Telefon Numaranızı Giriniz")]
        [Phone(ErrorMessage = "Lütfen telefon numarasını doğru giriniz")]
        public string Phone { get; set; }

        [Required,StringLength (50)]
        public string kullaniciEmail{ get; set; }
        [Required,StringLength(100)]
        public string kullaniciSifre { get; set; }
        public bool Locked { get; set; }=false;
        public DateTime KayitTarih { get; set; }= DateTime.UtcNow;

        [StringLength(255)]
        public string ProfilImg { get; set; } = "no_img.jpg";

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }


}
