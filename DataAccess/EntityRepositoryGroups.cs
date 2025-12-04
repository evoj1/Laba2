using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EntityRepositoryGroups<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;
        public EntityRepositoryGroups(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Create(T obj)
        {
            _dbSet.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<T> ReadAll()
        {
            return _dbSet.ToList();
        }

        public T ReadById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
