using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceImplementList.Implementations;

namespace ZakusochnayaViewWeb
{
    public static class Globals
    {
        public static IPokupatelService PokupatelService { get; } = new PokupatelServiceList();
        public static IElementService ElementService { get; } = new ElementServiceList();
        public static IOutputService OutputService { get; } = new OutputServiceList();
        public static IMainService MainService { get; } = new MainServiceList();
        public static ISkladService SkladService { get; } = new SkladServiceList();
    }
}