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
        public string Soyad { get; set; }
        public string Numara { get; set; }

        public bool Tur { get; set; }
        public int Depozito { get; set; }

        public int Borc { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }
    }
}