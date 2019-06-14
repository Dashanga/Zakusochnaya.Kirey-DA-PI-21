using System.Web.Mvc;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;

namespace ZakusochnayaViewWeb.Controllers
{
    public class AddSkladElementController : Controller
    {
        private IElementService ingredientService = Globals.ElementService;
        private ISkladService storageService = Globals.SkladService;
        private IMainService mainService = Globals.MainService;

        public ActionResult Index()
        {
            var ingredients = new SelectList(ingredientService.GetList(), "Id", "ElementName");
            ViewBag.Elements = ingredients;

            var storages = new SelectList(storageService.GetList(), "Id", "SkladName");
            ViewBag.Sklads = storages;
            return View();
        }

        [HttpPost]
        public ActionResult AddElementPost()
        {
            mainService.PutComponentOnStock(new SkladElementBindingModel
            {
                ElementId = int.Parse(Request["ElementId"]),
                SkladId = int.Parse(Request["SkladId"]),
                Number = int.Parse(Request["Number"])
            });
            return RedirectToAction("Index", "Home");
        }
    }
}
