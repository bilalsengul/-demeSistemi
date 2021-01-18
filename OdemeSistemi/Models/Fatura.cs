using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OdemeSistemi.Models
{
    public class Fatura
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Tutar { get; set; }
        [Required]
        public DateTime Tarih { get; set; }
        [Required]
        public bool OdemeDurum { get; set; }
        
        public Abone Abone { get; set; }
        public int AboneId { get; set; }
    }
}