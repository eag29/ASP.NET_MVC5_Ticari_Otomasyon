using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicariOtomasyon.Models.Siniflar;


namespace TicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context _context = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Partial1(Cari cari)
        {
            _context.Caris.Add(cari);
            _context.SaveChanges();
            return PartialView();
        }
        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariLogin1(Cari cari)
        {
            var _cari = _context.Caris.FirstOrDefault(x => x.CariMail == cari.CariMail & x.Sifre == cari.Sifre);
            if (_cari != null)
            {
                FormsAuthentication.SetAuthCookie(_cari.CariMail, false);
                Session["CariMail"] = _cari.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            var bilgiler = _context.Admins.FirstOrDefault(x => x.KullaniciAd == admin.KullaniciAd && admin.Sifre == x.Sifre);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(admin.KullaniciAd, false);
                Session["KullaniciAd"] = admin.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}