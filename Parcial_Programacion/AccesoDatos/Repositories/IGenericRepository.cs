using System;
using System.Collections.Generic;

namespace AccesoDatos.Repositories
{
    public interface IGenericRepository<T>
    {
        List<T> ObtenerTodos();
        T ObtenerPorId(int id);
        void Agregar(T entidad);
        void Eliminar(T entidad);
    }
}