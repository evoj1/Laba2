using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DapperRepositoryGroups<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly DataContext _context;

        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ReadAll()
        {
            throw new NotImplementedException(); //dsfgsdf
        }

        public T ReadById(int id)
        {
            throw new NotImplementedException(); //dsfdsf
        }
    }
}
