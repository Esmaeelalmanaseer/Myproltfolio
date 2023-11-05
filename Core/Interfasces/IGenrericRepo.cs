using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfasces
{
    public interface IGenrericRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void delete(object id);
    }
}
