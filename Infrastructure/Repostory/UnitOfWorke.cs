using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfasces;
using Infrastructure.Repostory;

namespace Infrastructure.Repostory
{
    public class UnitOfWorke<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenrericRepo<T> _entity;

        public UnitOfWorke(DataContext context)
        {
            this._context = context;
        }
        public IGenrericRepo<T> Entity 
        {
            get
            {
                return _entity ?? (_entity = new GenericRepo<T>(_context));
            }
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
