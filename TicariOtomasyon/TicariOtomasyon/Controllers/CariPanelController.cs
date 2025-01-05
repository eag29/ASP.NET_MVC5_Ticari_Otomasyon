using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context _context = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = _context.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;

            var mailid = _context.Caris.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            ViewBag.mailid = mailid;

            var toplamSatis = _context.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.tsatis = toplamSatis;

            var toplamTutar = _context.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.toplamTutar = toplamTutar;

            var toplamUrunSayisi = _context.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.UrunSayisi = toplamUrunSayisi;

            var adsoyad = _context.Caris.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adSoyad = adsoyad;

            return View(degerler);
        }

        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = _context.Caris.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = _context.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }

        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = _context.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(y => y.MesajlarID).ToList();

            var gelensayisi = _context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = _context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(mesajlar);
        }

        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = _context.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(y => y.MesajlarID).ToList();

            var gelensayisi = _context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = _context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {
            var degerler = _context.Mesajlars.Where(x => x.MesajlarID == id).ToList();
            var mail = (string)Session["CariMail"];

            var gelensayisi = _context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = _context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];

            var gelensayisi = _context.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = _context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesajlar)
        {
            var mail = (string)Session["CariMail"];
            mesajlar.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

            mesajlar.Gonderici = mail;
            _context.Mesajlars.Add(mesajlar);
            _context.SaveChanges();
            return View();
        }
        public ActionResult KargoTakip(string p)
        {
            var kargo = from x in _context.KargoDetays select x;
            kargo = kargo.Where(y => y.TakipKodu.Contains(p));
            return View(kargo.ToList());
        }
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = _context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = _context.Caris.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var caribul = _context.Caris.Find(id);
            return PartialView("Partial1", caribul);
        }
        public PartialViewResult Partial2()
        {
            var veriler = _context.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }
        public ActionResult CariBilgiGuncelle(Cari cari)
        {
            var cariBilgi = _context.Caris.Find(cari.CariID);
            cariBilgi.CariAd = cari.CariAd;
            cariBilgi.CariSoyad = cari.CariSoyad;
            cariBilgi.CariMail = cari.CariMail;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}