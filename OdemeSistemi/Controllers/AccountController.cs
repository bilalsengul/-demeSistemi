using OdemeSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdemeSistemi.Controllers
{
    public class AccountController : Controller
    {
        private ContextDb db;
        public AccountController()
        {
            db = new ContextDb();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Register()
        //{
        //    if(Session["CurrentAbone"]==null&& Session["CurrentGise"] != null)
        //    {
        //        return View();
        //    }
        //    return View(@"~/Views/Home/Error.cshtml");
        //}

        //[HttpPost]
        //public ActionResult Register(Abone model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        model.GiseId = 1;
        //        var result = db.Abones.Add(model);
        //        db.SaveChanges();
        //        return RedirectToAction("index", "Abones");
        //    }
        //    return View(model);
        //}

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model != null)
            {
                var result = db.Abones.FirstOrDefault(i => i.KullaniciAdi == model.KullaniciAdi && i.Sifre == model.Sifre);
                if (result != null)
                {
                    Session["CurrentAbone"] = result;
                    Session["CurrentGise"] = null;
                    return Redirect("/home/index");
                }                  
                else
                {

                    ModelState.AddModelError("user not found", "Kullanıcı adı veya şifre hatalıdır !");
                    return View(model);
                    //foreach(ModelState modelstate in ViewData.ModelState.Values)
                    //{
                    //    foreach (ModelError error in modelstate.Errors)
                    //    {

                    //    }
                    //}

                }
            }
            return View(model);
        }



        [HttpGet]
        public ActionResult GiseLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GiseLogin(LoginModel model)
        {
            if (model != null)
            {
                var result = db.Gises.FirstOrDefault(i => i.KullaniciAdi == model.KullaniciAdi && i.Sifre == model.Sifre);
                if (result != null)
                {
                    Session["CurrentAbone"] = null;
                    Session["CurrentGise"] = result;
                    return RedirectToAction("index", "Abones");
                }
                else
                {

                    ModelState.AddModelError("user not found", "Kullanıcı adı veya şifre hatalıdır !");
                    return View(model);
                    //foreach(ModelState modelstate in ViewData.ModelState.Values)
                    //{
                    //    foreach (ModelError error in modelstate.Errors)
                    //    {

                    //    }
                    //}

                }
            }
            return View(model);
        }
        
        public ActionResult Logout()
        {
            Session["CurrentAbone"] = null;

            Session["CurrentGise"] = null;
            return View();
        }
    }
}