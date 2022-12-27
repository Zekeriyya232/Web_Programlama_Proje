﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProje1.Models
{
    public class EditKullaniciVM
    {
        [Required(ErrorMessage = "İsminizi Giriniz")]
        public string kullaniciAdi { get; set; }

        [Required(ErrorMessage = "Soyadinizi Giriniz")]
        public string kullaniciSoyadi { get; set; }

        [Required(ErrorMessage = "Telefon Numaranızı Giriniz")]
        [Phone(ErrorMessage = "Lütfen telefon numarasını doğru giriniz")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Doğum Gününüzü Giriniz")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime kullaniciDogum { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public bool Locked { get; set; } = false;


        [Required(ErrorMessage = "Email Adresinizi Giriniz")]
        [StringLength(30)]
        public string kullaniciEmail { get; set; }

       
        
    }
}
