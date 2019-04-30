using PizzeriaWebView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZakusochnayaServiceDAL.Interfaces;

namespace ZakusochnayaViewWeb.Controllers
{
    public class OutputsController : Controller
    {
        public IOutputService service = Globals.OutputService;
        // GET: Pizzas
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}