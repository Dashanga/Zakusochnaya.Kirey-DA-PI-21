using PizzeriaWebView;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaViewWeb.Controllers
{
    public class OutputController : Controller
    {
        private IOutputService service = Globals.OutputService;
        private IElementService ingredientService = Globals.ElementService;

        // GET: Pizzas
        public ActionResult Index()
        {
            if (Session["Output"] == null)
            {
                var output = new OutputViewModel();
                output.OutputElements = new List<OutputElementViewModel>();
                Session["Output"] = output;
            }
            return View((OutputViewModel)Session["Output"]);
        }

        public ActionResult AddElement()
        {
            var ingredients = new SelectList(ingredientService.GetList(), "Id", "ElementName");
            ViewBag.Elements = ingredients;
            return View();
        }

        [HttpPost]
        public ActionResult AddElementPost()
        {
            var output = (OutputViewModel)Session["Output"];
            var ingredient = new OutputElementViewModel
            {
                Id = int.Parse(Request["Id"]),
                ElementName = ingredientService.GetElement(int.Parse(Request["Id"])).ElementName,
                Number = int.Parse(Request["Number"])
            };
            output.OutputElements.Add(ingredient);
            Session["Output"] = output;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateOutputPost()
        {
            var output = (OutputViewModel)Session["Output"];
            var outputIngredeints = new List<OutputElementBindingModel>();
            for (int i = 0; i < output.OutputElements.Count; ++i)
            {
                outputIngredeints.Add(new OutputElementBindingModel
                {
                    Id = output.OutputElements[i].Id,
                    OutputId = output.OutputElements[i].OutputId,
                    ElementId = output.OutputElements[i].ElementId,
                    Number = output.OutputElements[i].Number
                });
            }
            service.AddElement(new OutputBindingModel
            {
                OutputName = Request["OutputName"],
                Cost = Convert.ToDecimal(Request["Cost"]),
                OutputElements = outputIngredeints
            });
            Session.Remove("Output");
            return RedirectToAction("Index", "Outputs");
        }
    }
}