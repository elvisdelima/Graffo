namespace Graffo.Entidades
{
    public class Board : ImportableEntity
    {
        public virtual string IdOrganization { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
