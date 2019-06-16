using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Web.Http;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        public MainController(IMainService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void CreateZakaz(ZakazBindingModel model)
        {
            _service.CreateOrder(model);
        }
        [HttpPost]
        public void TakeZakazInWork(ZakazBindingModel model)
        {
            _service.TakeOrderInWork(model);
        }
        [HttpPost]
        public void FinishOrder(ZakazBindingModel model)
        {
            _service.FinishOrder(model);
        }
        [HttpPost]
        public void PayZakaz(ZakazBindingModel model)
        {
            _service.PayOrder(model);
        }
        [HttpPost]
        public void PutComponentOnSklad(SkladElementBindingModel model)
        {
            _service.PutComponentOnStock(model);
        }
    }
}