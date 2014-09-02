using System.Data.Entity;

namespace Graffo.Data
{
    public class GraffoContext : DbContext
    {
        public GraffoContext() : base("GraffoDataBase") { }

        public DbSet<Entidades.Importation> Importations { get; set; }
        public DbSet<Entidades.Organization> Organizations { get; set; }
        public DbSet<Entidades.Board> Boards { get; set; }
        public DbSet<Entidades.Column> Columns { get; set; }
        public DbSet<Entidades.Card> Cards { get; set; }
        public DbSet<Entidades.Member> Members { get; set; }
        public DbSet<Entidades.CardMember> CardMembers { get; set; }
    }
}
