namespace Graffo.Entidades
{
    public class Column : ImportableEntity
    {
        public virtual string IdBoard { get; set; }
        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
        public virtual bool? IsArchived { get; set; }
    }
}
