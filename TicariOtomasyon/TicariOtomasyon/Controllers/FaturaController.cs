using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context _context = new Context();

        public ActionResult Index()
        {
            var faturalar = _context.Faturas.ToList();
            return View(faturalar);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Fatura fatura)
        {
            _context.Faturas.Add(fatura);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FaturaGetir(int id)
        {
            var fatura = _context.Faturas.Find(id);
            return View("FaturaGetir", fatura);
        }
        [HttpPost]
        public ActionResult FaturaGuncelle(Fatura _fatura)
        {
            var fatura = _context.Faturas.Find(_fatura.FaturaID); ;
            fatura.FaturaSeriNo = _fatura.FaturaSeriNo;
            fatura.FaturaSıraNo = _fatura.FaturaSıraNo;
            fatura.Saat = _fatura.Saat;
            fatura.Tarih = _fatura.Tarih;
            fatura.VergiDairesi = _fatura.VergiDairesi;
            fatura.TeslimEden = _fatura.TeslimEden;
            fatura.TeslimAlan = _fatura.TeslimAlan;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = _context.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        public ActionResult YeniKalem(FaturaKalem faturaKalem)
        {
            _context.FaturaKalems.Add(faturaKalem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = _context.Faturas.ToList();
            cs.deger2 = _context.FaturaKalems.ToList();
            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Fatura fatura = new Fatura();
            fatura.FaturaSeriNo = FaturaSeriNo;
            fatura.FaturaSıraNo = FaturaSıraNo;
            fatura.Tarih = Tarih;
            fatura.VergiDairesi = VergiDairesi;
            fatura.Saat = Saat;
            fatura.TeslimAlan = TeslimAlan; 
            fatura.TeslimEden = TeslimEden;
            fatura.Toplam = decimal.Parse(Toplam);
            _context.Faturas.Add(fatura);

            foreach (var item in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = item.Aciklama;
                fk.BirimFiyat = item.BirimFiyat;
                fk.Faturaid = item.Faturaid;
                fk.Miktar = item.Miktar;
                fk.Tutar = item.Tutar;
                _context.FaturaKalems.Add(fk);
            }

            _context.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    } 
}