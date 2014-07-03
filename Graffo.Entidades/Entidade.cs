using Graffo.Entidades.Interfaces;

namespace Graffo.Entidades
{
    public abstract class Entidade : IEntidade
    {
        public int Id { get; set; }
    }
}
