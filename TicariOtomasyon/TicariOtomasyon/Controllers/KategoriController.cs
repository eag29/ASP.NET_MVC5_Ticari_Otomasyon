using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace TicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        Context _context = new Context();

        public ActionResult Index(int sayfa = 1)
        {
            var kategoriler = _context.Kategoris.ToList().ToPagedList(sayfa, 5);
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            _context.Kategoris.Add(kategori);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = _context.Kategoris.Find(id);
            _context.Kategoris.Remove(ktg);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = _context.Kategoris.Find(id);
            return View("KategoriGetir", kategori);
        }
        public ActionResult KategoriGuncelle(Kategori kategori)
        {
            var ktgr = _context.Kategoris.Find(kategori.KategoriID);
            ktgr.KategoriAd = kategori.KategoriAd;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(_context.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(_context.Uruns, "UrunID", "UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunListesi = (from x in _context.Uruns
                               join y in _context.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.UrunID.ToString()
                               }).ToList();
            return Json(urunListesi,JsonRequestBehavior.AllowGet);
        }
    }
}