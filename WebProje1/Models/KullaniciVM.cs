using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProje1.Models
{
    public class KullaniciVM
    {
        
        public int Id { get; set; }
        public string kullaniciAdi { get; set; }
        
        public string KullaniciSoyadi { get; set; }

        public string kullaniciEmail { get; set; }
        public string Phone { get; set; }
      
        public bool Locked { get; set; } = false;


        public DateTime KayitTarih { get; set; } = DateTime.UtcNow; //düzelt

        public string Role { get; set; } = "user";
    }
}
