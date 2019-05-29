using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Threading;

namespace ZakusochnayaRestApi.Services
{
    public class WorkExecutor
    {
        private readonly IMainService _service;
        private readonly IExecutorService _serviceImplementer;
        private readonly int _implementerId;
        private readonly int _orderId;
        // семафор
        static Semaphore _sem = new Semaphore(3, 3);
        Thread myThread;
        public WorkExecutor(IMainService service, IExecutorService
       serviceImplementer, int implementerId, int orderId)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
            _implementerId = implementerId;
            _orderId = orderId;
            try
            {
                _service.TakeOrderInWork(new ZakazBindingModel
                {
                    Id = _orderId,
                    ExecutorId = _implementerId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            myThread = new Thread(Work);
            myThread.Start();
        }
        public void Work()
        {
            try
            {
                // забиваем мастерскую
                _sem.WaitOne();
                // Типа выполняем
                Thread.Sleep(100000);
                _service.FinishOrder(new ZakazBindingModel
                {
                    Id = _orderId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // освобождаем мастерскую
                _sem.Release();
            }
        }
    }
}