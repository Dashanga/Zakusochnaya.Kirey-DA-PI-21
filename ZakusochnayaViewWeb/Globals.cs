using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceImplementDataBase;
using ZakusochnayaServiceImplementDataBase.Implementations;
using ZakusochnayaServiceImplementList.Implementations;


namespace ZakusochnayaViewWeb
{
    public static class Globals
    {
        public static ZakusochnayaDbContext DbContext { get; } = new ZakusochnayaDbContext();
        public static IPokupatelService PokupatelService { get; } = new PokupatelServiceDB(DbContext);
        public static IElementService ElementService { get; } = new ElementServiceDB(DbContext);
        public static IOutputService OutputService { get; } = new OutputServiceDB(DbContext);
        public static IMainService MainService { get; } = new MainServiceDB(DbContext);
        public static ISkladService SkladService { get; } = new SkladServiceDB(DbContext);
    }
}