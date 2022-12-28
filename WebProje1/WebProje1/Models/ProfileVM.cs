using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje1.Models
{
    public class ProfileVM
    {
        
        public string kullaniciAdi { get; set; }

        public string kullaniciSoyadi { get; set; }

        public string kullaniciEmail { get; set; }
        public string Phone { get; set; }

        public DateTime kullaniciDogum { get; set; }

        public DateTime KayitTarih { get; set; }  //düzelt

        [StringLength(255)]
        public string ProfilImg { get; set; } 

        
    }
}
