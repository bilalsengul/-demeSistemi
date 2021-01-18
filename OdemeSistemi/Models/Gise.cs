using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdemeSistemi.Models
{
    public class Gise
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string  Sifre { get; set; }
        public List<Abone>Aboneler { get; set; }
    }
}