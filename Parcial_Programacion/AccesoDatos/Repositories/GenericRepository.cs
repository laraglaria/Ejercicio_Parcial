using AccesoDatos.Data;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    where T : class
    {
        protected readonly MusicaDbContext _context;

        public GenericRepository(MusicaDbContext context)
        {
            _context = context;
        }

        public List<T> ObtenerTodos()
        {
            return _context.Set<T>().ToList();
        }

        public T ObtenerPorId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Agregar(T entidad)
        {
            _context.Set<T>().Add(entidad);
            _context.SaveChanges();
        }

        public void Eliminar(T entidad)
        {
            _context.Set<T>().Remove(entidad);
            _context.SaveChanges();
        }
    }
}