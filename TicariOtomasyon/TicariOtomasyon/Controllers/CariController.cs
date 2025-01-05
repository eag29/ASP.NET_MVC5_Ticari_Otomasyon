using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context _context = new Context();
        public ActionResult Index()
        {
            var degerler = _context.Caris.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cari cari)
        {
            _context.Caris.Add(cari);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var cari = _context.Caris.Find(id);
            _context.Caris.Remove(cari);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var cari = _context.Caris.Find(id);
            return View("CariGetir", cari);
        }
        public ActionResult CariGuncelle(Cari _cari)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cari = _context.Caris.Find(_cari.CariID);
            cari.CariAd = _cari.CariAd;
            cari.CariSoyad = _cari.CariSoyad;
            cari.CariSehir = _cari.CariSehir;
            cari.CariMail = _cari.CariMail;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var degerler = _context.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cr = _context.Caris.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        }
    }
}