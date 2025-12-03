using Dapper;
using DataAccess.Migrations;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DapperRepositoryStudents<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly string _connectionString;
        public DapperRepositoryStudents()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
        }
        public void Create(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (item is Student s)
                {
                    connection.Execute(
                        "INSERT INTO Students (Name, Specialty, GroupId) VALUES (@Name, @Specialty, @GroupId)", s);
                }
            }
        }

        public void Delete(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (item is Student s)
                {
                    connection.Execute("DELETE FROM Students WHERE Id = @Id", new { Id = s.Id });
                }
            }
        }

        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (typeof(T) == typeof(Student))
                {
                    return connection.Query<Student, Groups, Student>(
                        @"SELECT s.*, g.Id, g.Name FROM Students s INNER JOIN Groups g ON s.GroupId = g.Id",
                        (student, group) => { student.Group = group; return student; }, splitOn: "Id").Cast<T>().ToList();
                }
                else if (typeof(T) == typeof(Groups))
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
                if (typeof(T) == typeof(Student))
                {
                    return connection.QuerySingleOrDefault<Student>(
                        "SELECT * FROM Students WHERE Id = @Id", new { Id = id }) as T;
                }
                throw new NotImplementedException("ReadById реализован для Student!");
            }
        }
    }
}
