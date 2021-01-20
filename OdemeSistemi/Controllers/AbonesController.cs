using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OdemeSistemi.Models;

namespace OdemeSistemi.Controllers
{
    public class AbonesController : Controller
    {
        private ContextDb db;
       // private string[] hata;
        public AbonesController()
        {
            db = new ContextDb();
          //  hata = new string[1];
        }
        // GET: Abones
        public ActionResult Index()
        {
            if (Session["CurrentAbone"] == null && Session["CurrentGise"] != null)
            {
                if (Session["hata"] != null)
                    ViewBag.error = Session["hata"];
                //bu kontrol sadece gise görebilir
                var abones = db.Abones.Include(a => a.Gise);
                return View(abones.ToList());
            }
            return View(@"~/Views/Home/Error.cshtml");
        }

        // GET: Abones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abone abone = db.Abones.Find(id);
            if (abone == null)
            {
                return HttpNotFound();
            }
            return View(abone);
        }

        // GET: Abones/Create
        public ActionResult Create()
        {

            if (Session["CurrentAbone"] == null && Session["CurrentGise"] != null)
            {
                ViewBag.GiseId = new SelectList(db.Gises, "Id", "KullaniciAdi");
                return View();
            }

            return View(@"~/Views/Home/Error.cshtml");
        }

        // POST: Abones/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad,Soyad,Numara,Tur,Depozito,Borc,KullaniciAdi,Sifre,GiseId")] Abone abone)
        {
            if (ModelState.IsValid)
            {
                db.Abones.Add(abone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GiseId = new SelectList(db.Gises, "Id", "KullaniciAdi", abone.GiseId);
            return View(abone);
        }

        // GET: Abones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abone abone = db.Abones.Find(id);
            if (abone == null)
            {
                return HttpNotFound();
            }
            ViewBag.GiseId = new SelectList(db.Gises, "Id", "KullaniciAdi", abone.GiseId);
            return View(abone);
        }

        // POST: Abones/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,Soyad,Numara,Tur,Depozito,Borc,KullaniciAdi,Sifre,GiseId")] Abone abone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiseId = new SelectList(db.Gises, "Id", "KullaniciAdi", abone.GiseId);
            return View(abone);
        }

        

        // POST: Abones/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abone abone = db.Abones.Find(id);
            if (abone == null)
            {
                return HttpNotFound();
            }
            abone.Faturalar = db.Faturas.Where(i=>i.AboneId==abone.Id).ToList();
            if (abone.Faturalar != null)
            {
                int borc = abone.Borc;
                foreach (var item in abone.Faturalar)
                {
                    if (item.OdemeDurum == false && item.Tarih < DateTime.Now)
                    {
                        borc += item.Tutar;
                    }
                }

                if(abone.Depozito ==borc)
                {
                    abone.Depozito=abone.Depozito - borc;
                    foreach (var fatura in db.Faturas.Where(i => i.AboneId == abone.Id).ToList())
                    {
                        db.Faturas.Remove(fatura);
                    }

                    db.Abones.Remove(abone);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (abone.Depozito > borc)
                {
                    abone.Depozito = abone.Depozito - borc;
                    foreach (var fatura in db.Faturas.Where(i => i.AboneId == abone.Id).ToList())
                    {
                        db.Faturas.Remove(fatura);
                    }
                    //ViewBag.iade = "İade edilecek tutar : " + abone.Depozito;
                    TempData["iade"] = "İade edilecek tutar : " + abone.Depozito;
                    db.Abones.Remove(abone);
                    db.SaveChanges();
                  
                    return RedirectToAction("Index");
                    
                }
            }
            
           Session["hata"] ="Depozito miktarı borcunuzu odeyecek tutarda degil !";
         
           return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            if (model.tc != null)
            {
                if (model.tc == null || model.tc.Equals(""))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var abone = db.Abones.FirstOrDefault(i => i.Numara == model.tc);
                if (abone != null)
                {
                    return View(abone);
                }
                ViewBag.error = "TC numaralı abone bulunumadı ! ";
            }
            else if (model.vergi != null)
            {

                if (model.vergi == null || model.vergi.Equals(""))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var abone = db.Abones.FirstOrDefault(i => i.Numara == model.vergi);
                if (abone != null)
                {
                    return View(abone);
                }
                ViewBag.error = "Vergi numaralı abone bulunumadı ! ";
            }
           
            return View();
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
