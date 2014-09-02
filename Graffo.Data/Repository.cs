using System.Linq;
using Graffo.Data.Interfaces;
using Graffo.Entidades;
using System.Data.Entity;

namespace Graffo.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected GraffoContext context;
        protected bool disposing;

        public Repository(GraffoContext context)
        {
            this.context = context;
        }

        public T Add(T entity)
        {
            var add = context.Set<T>().Add(entity);
            return add;
        }

        public void Update(T entity)
        {
            context.Set<T>().Attach(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Delete(long id)
        {
            var entity = context.Set<T>().Find(id);
            context.Set<T>().Remove(entity);
        }

        public long Total { get; private set; }
        public T Last { get; private set; }
        public virtual T Find(long id)
        {
            return context.Set<T>().Find(id);
        }

        public IQueryable<T> Query { get { return context.Set<T>().AsQueryable(); } }

        public bool Exists(long id)
        {
            return context.Set<T>().Any(arg => arg.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Detach(T entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }

        public void Dispose()
        {
            if (!disposing)
            {
                disposing = true;
                //SaveChanges();
            }
        }
    }
}
