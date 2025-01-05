using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {

        Context context = new Context();

        public ActionResult Index()
        {
            //var urunler = from x in context.Uruns select x;

            //if (!string.IsNullOrEmpty(urunara))
            //{
            //    urunler = urunler.Where(y => y.UrunAd.Contains(urunara));
            //}
            //return View(urunara);

            var urunler = context.Uruns.ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString(),
                                           }).ToList();
            ViewBag.Deger1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun urun)
        {
            context.Uruns.Add(urun);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger = context.Uruns.Find(id);
            context.Uruns.Remove(deger);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString(),
                                           }).ToList();
            ViewBag.Deger1 = deger1;

            var urundeger = context.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        public ActionResult UrunGuncelle(Urun urun)
        {
            var urn = context.Uruns.Find(urun.UrunID);
            urn.AlisFiyati = urun.AlisFiyati;
            urn.SatisFiyati = urun.SatisFiyati;
            urn.Marka = urun.Marka;
            urn.KategoriId = urun.KategoriId;
            urn.UrunAd = urun.UrunAd;
            urn.UrunGorsel = urun.UrunGorsel;
            urn.Stok = urun.Stok;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = context.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var deger2 = context.Uruns.Find(id);
            ViewBag.dgr2 = deger2.UrunID;
            ViewBag.dgr3 = deger2.SatisFiyati;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket satisHareket)
        {
            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHarekets.Add(satisHareket);
            context.SaveChanges();
            return RedirectToAction("Index", "Satis");
        }
    }
}