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
    public class FaturasController : Controller
    {
        private ContextDb db = new ContextDb();

        // GET: Faturas
        public ActionResult Index()
        {
            var faturas = db.Faturas.Include(f => f.Abone);
            return View(faturas.ToList());
        }

        // GET: Faturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fatura fatura = db.Faturas.Find(id);
            if (fatura == null)
            {
                return HttpNotFound();
            }
            return View(fatura);
        }

        // GET: Faturas/Create
        public ActionResult Create()
        {
            ViewBag.AboneId = new SelectList(db.Abones, "Id", "Ad");
            return View();
        }

        // POST: Faturas/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tutar,Tarih,OdemeDurum,AboneId")] Fatura fatura)
        {
            if (ModelState.IsValid)
            {
                db.Faturas.Add(fatura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AboneId = new SelectList(db.Abones, "Id", "Ad", fatura.AboneId);
            return View(fatura);
        }

        // GET: Faturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fatura fatura = db.Faturas.Find(id);
            if (fatura == null)
            {
                return HttpNotFound();
            }
            ViewBag.AboneId = new SelectList(db.Abones, "Id", "Ad", fatura.AboneId);
            return View(fatura);
        }

        // POST: Faturas/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tutar,Tarih,OdemeDurum,AboneId")] Fatura fatura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fatura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AboneId = new SelectList(db.Abones, "Id", "Ad", fatura.AboneId);
            return View(fatura);
        }

        // GET: Faturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fatura fatura = db.Faturas.Find(id);
            if (fatura == null)
            {
                return HttpNotFound();
            }
            return View(fatura);
        }

        // POST: Faturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fatura fatura = db.Faturas.Find(id);
            db.Faturas.Remove(fatura);
            db.SaveChanges();
            return RedirectToAction("Index");
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
