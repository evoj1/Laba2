using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IDomainObject
    {
        int Id { get; set; }
    }
    public class Student : IDomainObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Groups Group { get; set; } 
        public Student() { }
        public Student(string name, string specialty, int groupId)
        {
            Name = name;
            Specialty = specialty;
            GroupId = groupId;
        }
    }
}
