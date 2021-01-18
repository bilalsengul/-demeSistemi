using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdemeSistemi.Models
{
    public class Fatura
    {

        public int Id { get; set; }
        public int Tutar { get; set; }

        public DateTime Tarih { get; set; }
        public bool OdemeDurum { get; set; }

        public Abone Abone { get; set; }
        public int AboneId { get; set; }
    }
}