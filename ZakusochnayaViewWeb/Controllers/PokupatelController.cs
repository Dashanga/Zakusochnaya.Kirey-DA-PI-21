using PizzeriaWebView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceImplementList.Implementations;

namespace ZakusochnayaViewWeb.Controllers
{
    public class PokupatelController : Controller
    {
        public IPokupatelService service = Globals.PokupatelService;
        // GET: Customers
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new PokupatelBindingModel
            {
                PokupatelFIO = Request["PokupatelFIO"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new PokupatelBindingModel
            {
                PokupatelId = id,
                PokupatelFIO = viewModel.PokupatelFIO
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdElement(new PokupatelBindingModel
            {
                PokupatelId = int.Parse(Request["PokupatelId"]),
                PokupatelFIO = Request["PokupatelFIO"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}