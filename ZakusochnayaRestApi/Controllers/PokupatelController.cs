using System;
using System.Web.Http;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaRestApi.Controllers
{
    public class PokupatelController : ApiController
    {
        private readonly IPokupatelService _service;
        public PokupatelController(IPokupatelService service)
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
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }
        [HttpPost]
        public void AddElement(PokupatelBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(PokupatelBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(PokupatelBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}