using ZakusochnayaModel;
using System.Data.Entity;

namespace ZakusochnayaServiceImplementDataBase
{
    public class ZakusochnayaDbContext : DbContext
    {
        public ZakusochnayaDbContext() : base("ZakusochnayaDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
            System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Pokupatel> Pokupatels { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<Zakaz> Zakazs { get; set; }
        public virtual DbSet<Output> Outputs { get; set; }
        public virtual DbSet<OutputElement> OutputElements { get; set; }
        public virtual DbSet<Sklad> Sklads { get; set; }
        public virtual DbSet<SkladElement> SkladElements { get; set; }
        public virtual DbSet<Executor> Executors { get; set; }
    }
}
