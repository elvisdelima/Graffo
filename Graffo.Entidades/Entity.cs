using Graffo.Entidades.Interfaces;

namespace Graffo.Entidades
{
    public abstract class Entity : IEntity
    {
        public virtual long Id { get; set; }
    }
}
