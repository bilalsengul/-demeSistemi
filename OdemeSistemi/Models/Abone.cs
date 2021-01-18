using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OdemeSistemi.Models
{
    public class Abone
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string  Ad { get; set; }
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


        public List<Fatura> Faturalar { get; set; }
        public int GiseId { get; set; }
        public Gise Gise { get; set; }

    }
}