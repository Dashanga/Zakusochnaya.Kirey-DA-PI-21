using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceImplementList.Implementations;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using ZakusochnayaServiceDAL;

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
            currentContainer.RegisterType<IPokupatelService, PokupatelServiceList>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISkladService, SkladServiceList>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IElementService, ElementServiceList>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOutputService, OutputServiceList>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new
            HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
