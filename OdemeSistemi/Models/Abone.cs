using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdemeSistemi.Models
{
    public class Abone
    {
        public int Id { get; set; }
        public string  Ad { get; set; }
        public string Soyad { get; set; }
        public string Numara { get; set; }

        public bool Tur { get; set; }
        public int Depozito { get; set; }

        public int Borc { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }


        public List<Fatura> Faturalar { get; set; }
        public int GiseId { get; set; }
        public Gise Gise { get; set; }

    }
}