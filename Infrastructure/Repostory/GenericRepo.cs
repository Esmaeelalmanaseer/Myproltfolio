using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Interfasces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repostory
{
    public class GenericRepo<T> : IGenrericRepo<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;

        public GenericRepo(DataContext Context)
        {
            _context = Context;
            table = _context.Set<T>();
        }

        public void delete(object id)
        {
            T entity = table.Find(id);
              table.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
