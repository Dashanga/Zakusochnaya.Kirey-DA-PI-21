using PizzeriaWebView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaViewWeb.Controllers
{
    public class ZakazController : Controller
    {
        private IOutputService outputService = Globals.OutputService;
        private IMainService mainService = Globals.MainService;
        private IPokupatelService pokupatelService = Globals.PokupatelService;


        // GET: PizzaOrder
        public ActionResult Index()
        {
            return View(mainService.GetList());
        }

        public ActionResult Create()
        {
            var outputs = new SelectList(outputService.GetList(), "OutputId", "OutputName");
            var pokupatels = new SelectList(pokupatelService.GetList(), "PokupatelId", "PokupatelFIO");
            ViewBag.Outputs = outputs;
            ViewBag.Pokupatels = pokupatels;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            var customerId = int.Parse(Request["PokupatelId"]);
            var outputId = int.Parse(Request["OutputId"]);
            var Count = int.Parse(Request["Number"]);
            var totalCost = CalcSum(outputId, Count);

            mainService.CreateOrder(new ZakazBindingModel
            {
                PokupatelId = customerId,
                OutputId = outputId,
                Number = Count,
                Summa = totalCost

            });
            return RedirectToAction("Index");
        }

        private Decimal CalcSum(int outputId, int Count)
        {
            OutputViewModel output = outputService.GetElement(outputId);
            return Count * output.Cost;
        }

        public ActionResult SetStatus(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Processing":
                        mainService.TakeOrderInWork(new ZakazBindingModel { ZakazId = id });
                        break;
                    case "Ready":
                        mainService.FinishOrder(new ZakazBindingModel { ZakazId = id });
                        break;
                    case "Paid":
                        mainService.PayOrder(new ZakazBindingModel { ZakazId = id });
                        break;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }


            return RedirectToAction("Index");
        }
    }
}