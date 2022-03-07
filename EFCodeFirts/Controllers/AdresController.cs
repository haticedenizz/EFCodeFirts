using EFCodeFirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFCodeFirts.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        public ActionResult Yeni()
        {
            DatabaseContext db = new DatabaseContext();
            List<SelectListItem> kisiliste =
                (from kisi in db.Kisiler.ToList()
                 select new SelectListItem()
                 {
                     Text = kisi.Ad + " " + kisi.Soyad,
                     Value = kisi.ID.ToString()
                 }).ToList();
            //List<Kisiler> kisiler = db.Kisiler.ToList();
            //List<SelectListItem> kisiliste = new List<SelectListItem>();
            //foreach (var kisi in kisiler)
            //{
            //    SelectListItem item = new SelectListItem();
            //    item.Text = kisi.Ad+" "+kisi.Soyad;
            //    item.Value = kisi.ID.ToString();
            //    kisiliste.Add(item);

            //}
            TempData["kisiler"] = kisiliste;


            ViewBag.kisiler = kisiliste;

            return View();
        }
        [HttpPost]
        public ActionResult Yeni(Adresler adres)
        {
            DatabaseContext db = new DatabaseContext();
            Kisiler kisi =
            db.Kisiler.Where(x => x.ID == adres.Kisi.ID).FirstOrDefault();
            adres.Kisi = kisi;
            db.Adresler.Add(adres);
            ViewBag.kisiler = TempData["kisiler"];

            int sonuc = db.SaveChanges();
            if (sonuc > 0)
            {
                ViewBag.mesaj = "Kişi kaydedildi.";
                ViewBag.renk = "success";

            }

            else
            {
                ViewBag.mesaj = "Kişi kayıt yapılırken bir hata oluştu.";
                ViewBag.renk = "danger";

            }
            return View();
        }
        public ActionResult Duzenle(int? adresid)
        {
            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();

            if (adresid!=null)
            {
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
            }
           
            List<SelectListItem> kisiliste =
               (from kisi in db.Kisiler.ToList()
                select new SelectListItem()
                {
                    Text = kisi.Ad + " " + kisi.Soyad,
                    Value = kisi.ID.ToString()
                }).ToList();
            TempData["kisiler"] = kisiliste;
            ViewBag.kisiler = kisiliste;

            return View(adres);
        }
        [HttpPost]
        public ActionResult Duzenle(Adresler adres, int? adresid)
        {
            Adresler a = new Adresler();
            DatabaseContext db = new DatabaseContext();
            if (adresid !=null)
            {
              a = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();

                Kisiler k = db.Kisiler.Where(x => x.ID == adres.Kisi.ID).FirstOrDefault();
                a.Kisi = k;
                a.AdresTanim = adres.AdresTanim;


                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.mesaj = "Değisiklikler kaydedildi.";
                    ViewBag.renk = "success";

                }

                else
                {
                    ViewBag.mesaj = "Değisiklikler yapılırken bir hata oluştu.";
                    ViewBag.renk = "danger";

                }
            }
           
            ViewBag.kisiler= TempData["kisiler"];
            return View(a);
        }
        public ActionResult Sil(int? adresid)
        {
            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();
            if (adresid != null)
            {
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
            }

            return View(adres);

        }
        [HttpPost, ActionName("Sil")]
        public ActionResult KisiSil(int? adresid)
        {
            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();
            if (adresid != null)
            {
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
                db.Adresler.Remove(adres);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");

        }
    }
}