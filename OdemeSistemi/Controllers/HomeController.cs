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
        // GET: Home
        public ActionResult Index()
        {
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