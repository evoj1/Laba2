using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T>
    {
        void Create(T item);
        IEnumerable<T> ReadAll();
        T ReadById (int id);
        void Delete(T item);
    }
}
