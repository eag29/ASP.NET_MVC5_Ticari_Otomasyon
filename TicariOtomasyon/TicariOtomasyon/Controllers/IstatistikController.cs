using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        Context _context = new Context();
        public ActionResult Index()
        {
            var toplamCariSayisi = _context.Caris.Count().ToString();
            ViewBag.d1 = toplamCariSayisi;

            var toplamUrunSayisi = _context.Uruns.Count().ToString();
            ViewBag.d2 = toplamUrunSayisi;

            var toplamPersonelSayisi = _context.Personels.Count().ToString();
            ViewBag.d3 = toplamPersonelSayisi;

            var toplamKategoriSayisi = _context.Kategoris.Count().ToString();
            ViewBag.d4 = toplamKategoriSayisi;


            var toplamStokSayisi = _context.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = toplamStokSayisi;

            var toplamMarkaSayisi = (from x in _context.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = toplamMarkaSayisi;

            var kritikSeviye = _context.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = kritikSeviye;

            var enYuksekFiyatliUrun = (from x in _context.Uruns orderby x.SatisFiyati descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = enYuksekFiyatliUrun;


            var enDusukFiyatliUrun = (from x in _context.Uruns orderby x.SatisFiyati ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = enDusukFiyatliUrun;

            var maxMarka = _context.Uruns.GroupBy(x => x.Marka).OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault();
            ViewBag.d10 = maxMarka;

            var buzdolabiSayisi = _context.Uruns.Count(x => x.UrunAd == "Buzdolabı");
            ViewBag.d11 = buzdolabiSayisi;

            var LaptopSayisi = _context.Uruns.Count(x => x.UrunAd == "Laptop");
            ViewBag.d12 = LaptopSayisi;


            var enCokSatanUrun = _context.Uruns.Where(u => u.UrunID == (_context.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = enCokSatanUrun;

            var kasadakiTutar = _context.SatisHarekets.Sum(x => x.ToplamTutar);
            ViewBag.d14 = kasadakiTutar;

            var bugunkuSatislar = _context.SatisHarekets.Count(x => x.Tarih == DateTime.Today).ToString();
            ViewBag.d15 = bugunkuSatislar;

            var bugunkuKasa = _context.SatisHarekets.Where(x => x.Tarih == DateTime.Today).Sum(y => (decimal?)y.ToplamTutar).ToString();
            ViewBag.d16 = bugunkuKasa;

            return View();
        }

        public ActionResult KolayTablolar()
        {
            var sorgu = from x in _context.Caris
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in _context.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrub2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var cariler = _context.Caris.ToList();
            return PartialView(cariler);
        }
        public PartialViewResult Partial3()
        {
            var sorgu = _context.Uruns.ToList();
            return PartialView(sorgu);
        }
        public PartialViewResult Partial4()
        {
            var sorgu3 = from x in _context.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             Marka = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu3.ToList());
        }
    }
}