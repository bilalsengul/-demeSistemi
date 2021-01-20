using OdemeSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdemeSistemi.Controllers
{
    public class HomeController : Controller
    {
        private ContextDb db = new ContextDb();
        // GET: Home
        public ActionResult Index()
        {
            if (db.Gises.Count() == 0)
            {
                Gise gise = new Gise();
                gise.KullaniciAdi = "gise";
                gise.Sifre = "gise";
                db.Gises.Add(gise);
                db.SaveChanges();
            }

            Abone abone = (Abone)Session["CurrentAbone"];
            if (abone == null)
            {
                ViewBag.isim = "deneme";
                return View();
            }
            ViewBag.isim = abone.Ad;
            return View();
        }
    }
}