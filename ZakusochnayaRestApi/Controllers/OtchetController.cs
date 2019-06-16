using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Web.Http;
namespace ZakusochnayaRestApi.Controllers
{
    public class OtchetController : ApiController
    {
        private readonly IOtchetService _service;
        public OtchetController(IOtchetService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetSkladsLoad()
        {
            var list = _service.GetStocksLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult GetPokupatelZakazs(OtchetBindingModel model)
        {
            var list = _service.GetClientOrders(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void SaveProductPrice(OtchetBindingModel model)
        {
            _service.SaveProductPrice(model);
        }
        [HttpPost]
        public void SaveSkladssLoad(OtchetBindingModel model)
        {
            _service.SaveStocksLoad(model);
        }
        [HttpPost]
        public void SavePokupatelZakazs(OtchetBindingModel model)
        {
            _service.SaveClientOrders(model);
        }
    }
}