using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdemeSistemi.Models
{
    public class RegisterModel
    {
        [Required]
        public string Ad { get; set; }
        [Required]
        public string Soyad { get; set; }
        [Required]
        public string Numara { get; set; }
        [Required]
        public bool Tur { get; set; }
        [Required]
        public int Depozito { get; set; }
        [Required]
        public int Borc { get; set; }
        [Required]
        public string KullaniciAdi { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
}