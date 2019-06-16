using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Web.Http;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModels;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaRestApi.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace ZakusochnayaRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        private readonly IExecutorService _serviceImplementer;
        public MainController(IMainService service, IExecutorService
       serviceImplementer)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
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
        public void CreateOrder(ZakazBindingModel model)
        {
            _service.CreateOrder(model);
        }
        [HttpPost]
        public void PayOrder(ZakazBindingModel model)
        {
            _service.PayOrder(model);
        }
        [HttpPost]
        public void PutComponentOnSklad(SkladElementBindingModel model)
        {
            
            _service.PutComponentOnSklad(model);
        }
        [HttpPost]
        public void StartWork()
        {
            List<ZakazViewModel> orders = _service.GetFreeOrders();
            foreach (var order in orders)
            {
                ExecutorViewModel impl = _serviceImplementer.GetFreeWorker();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkExecutor(_service, _serviceImplementer, impl.Id, order.Id);
            }
        }
        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
    }
}