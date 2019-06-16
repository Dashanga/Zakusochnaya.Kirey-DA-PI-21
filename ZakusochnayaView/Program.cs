using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceImplementDataBase;
using ZakusochnayaServiceImplementDataBase.Implementations;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceImplementList.Implementations;

namespace ZakusochnayaView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, ZakusochnayaDbContext>(new
 HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPokupatelService, PokupatelServiceDB>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IElementService, ElementServiceDB>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOutputService, OutputServiceDB>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISkladService, SkladServiceDB>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
            HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
