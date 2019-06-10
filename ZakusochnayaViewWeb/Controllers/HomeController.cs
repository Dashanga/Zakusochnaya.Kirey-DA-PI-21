using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceImplementList.Implementations;

namespace ZakusochnayaViewWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pokupatels()
        {
            return RedirectToAction("Index", "Pokupatel");
        }

        public ActionResult Elements()
        {
            return RedirectToAction("Index", "Element");
        }

        public ActionResult Outputs()
        {
            return RedirectToAction("Index", "Output");
        }

        public ActionResult Zakazs()
        {
            return RedirectToAction("Index", "Zakaz");
        }
    }
}