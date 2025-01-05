using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        Context _context = new Context();
        public ActionResult Index()
        {
            Class1 Class1 = new Class1();
            Class1.Deger1 = _context.Uruns.Where(x => x.UrunID == 1).ToList();
            Class1.Deger2 = _context.Detays.Where(x => x.DetayID == 1).ToList();
            return View(Class1);
        }
    }
}