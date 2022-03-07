using EFCodeFirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace EFCodeFirts.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DatabaseContext db = new DatabaseContext();

            List<Kisiler> kisiler=db.Kisiler.ToList();

            KisilerveAdresler model = new KisilerveAdresler();
            model.Kisiler = db.Kisiler.ToList();
            model.Adresler = db.Adresler.ToList();


            return View(model);
        }
    }
}