using System;
using System.Web.Http;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;

namespace ZakusochnayaRestApi.Controllers
{
    public class OutputElementController : ApiController
    {
        private readonly IOutputElementService _service;
        public OutputElementController(IOutputElementService service)
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
        public void AddElement(OutputElementBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(OutputElementBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(OutputElementBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}