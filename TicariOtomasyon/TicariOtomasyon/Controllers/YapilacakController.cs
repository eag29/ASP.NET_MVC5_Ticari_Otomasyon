using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        Context _context = new Context();
        public ActionResult Index()
        {
            var deger1 = _context.Caris.Count().ToString();
            ViewBag.d1 = deger1;

            var deger2 = _context.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 =_context.Kategoris.Count().ToString();
            ViewBag.d3 = deger3;

            var deger4 = (from x in _context.Caris select x.CariSehir).Distinct().Count().ToString();
            ViewBag.d4 = deger4;

            var yapilacaklar = _context.Yapilacaks.ToList();
            return View(yapilacaklar);
        }
    }
}