using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DapperRepositoryGroups<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly string _connectionString;
        public DapperRepositoryGroups()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
        }
        public void Create(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (item is Groups g)
                {
                    connection.Execute(
                        "INSERT INTO Groups (Id, Name) VALUES (@Id, @Name)", g);
                }
            }
        }

        public void Delete(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (item is Groups g)
                {
                    connection.Execute("DELETE FROM Groups WHERE Id = @Id", new { Id = g.Id });
                }
            }
        }

        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (typeof(T) == typeof(Groups))
                {
                    return connection.Query<Groups>("SELECT * FROM Groups").Cast<T>().ToList();
                }
                throw new NotImplementedException("ReadAll реализован только для Student и Groups!");
            }
        }


        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (typeof(T) == typeof(Groups))
                {
                    return connection.QuerySingleOrDefault<Groups>(
                        "SELECT * FROM Groups WHERE Id = @Id", new { Id = id }) as T;
                }
                throw new NotImplementedException("ReadById реализован для Student и Groups!");
            }
        }

    }
}
