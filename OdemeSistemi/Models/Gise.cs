using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OdemeSistemi.Models
{
    public class Gise
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string KullaniciAdi { get; set; }
        [Required]
        public string  Sifre { get; set; }
        public List<Abone>Aboneler { get; set; }
    }
}