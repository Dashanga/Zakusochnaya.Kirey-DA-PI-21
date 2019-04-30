using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceImplementList.Implementations;

namespace PizzeriaWebView
{
    public static class Globals
    {
        public static IPokupatelService PokupatelService { get; } = new PokupatelServiceList();
        public static IElementService ElementService { get; } = new ElementServiceList();
        public static IOutputService OutputService { get; } = new OutputServiceList();
        public static IMainService MainService { get; } = new MainServiceList();
    }
}