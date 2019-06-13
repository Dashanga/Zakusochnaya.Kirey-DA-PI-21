using PizzeriaWebView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;

namespace ZakusochnayaViewWeb.Controllers
{
    public class ElementController : Controller
    {
        private IElementService service = Globals.ElementService;
        // GET: Elements
        public ActionResult Index()
        {
            return View(service.GetList());
        }


        // GET: Elements/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new ElementBindingModel
            {
                ElementName = Request["ElementName"]
            });
            return RedirectToAction("Index");
        }


        // GET: Elements/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new ElementBindingModel
            {
                ElementId = id,
                ElementName = viewModel.ElementName
            };
            return View(bindingModel);
        }


        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdElement(new ElementBindingModel
            {
                ElementId = int.Parse(Request["ElementId"]),
                ElementName = Request["ElementName"]
            });
            return RedirectToAction("Index");
        }


        // GET: Elements/Delete/5
        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}
