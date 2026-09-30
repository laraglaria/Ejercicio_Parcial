using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos.Data;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class CancionRepository : GenericRepository<Cancion>
    {
        public CancionRepository(MusicaDbContext context)
            : base(context)
        {
        }

        public List<Cancion> ObtenerCancionesMasLargas()
        {
            return _context.Canciones
                .OrderByDescending(c => c.Duracion)
                .ToList();
        }

        public int ObtenerCantidadCanciones()
        {
            return _context.Canciones.Count();
        }

        public List<Cancion> ObtenerCancionesOrdenadas()
        {
            return _context.Canciones
                .OrderBy(c => c.Titulo)
                .ToList();
        }
        public bool ExistenCanciones()
        {
            return _context.Canciones.Any();
        }
    }
}