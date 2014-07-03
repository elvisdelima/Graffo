using System.Data.Entity;

namespace Graffo.Data
{
    public class GraffoContext : DbContext
    {
        public GraffoContext() : base("GraffoDataBase") { }

        public DbSet<Entidades.Data> Datas { get; set; }
    }
}
