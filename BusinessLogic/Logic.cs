using DataAccess;
using Model;      
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BusinessLogic
{
    public class Logic
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Groups> _groupRepository;
        public event Action DataChanged;
        public Logic(IRepository<Student> studentRepository, IRepository<Groups> groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }
        public void AddStudent(string name, string specialty, int groupId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Введите имя");
            if (string.IsNullOrWhiteSpace(specialty))
                throw new ArgumentException("Введите специальность");
            if (string.IsNullOrWhiteSpace(groupId.ToString()))
                throw new ArgumentException("Выберите группу");

            var student = new Student(name, specialty, groupId);
            _studentRepository.Create(student);
            DataChanged?.Invoke();
        }
        public bool RemoveStudent(int id)
        {
            var student = _studentRepository.ReadById(id);
            if (student == null)
                return false;
            _studentRepository.Delete(student);
            DataChanged?.Invoke();
            return true;
        }
        public List<string> GetAllGroups()
        {
            var groups = _groupRepository.ReadAll().ToList();
            var allGroups = new List<string>();
            foreach (var group in groups)
            {
                allGroups.Add($"{group.Id} {group.Name}");
            }
            return allGroups;
        }

        public List<string> PrintAllStudentsWithGroupNames()
        {
            using (var context = new DataContext())
            {
                var students = context.Students.Include(s => s.Group).ToList();
                var result = new List<string>();
                foreach (var student in students)
                {
                    result.Add($"{student.Id} | {student.Name} | {student.Specialty} | {student.Group.Name}");
                }
                return result;
            }
        }
        public List<string> PrintAllStudentsWithGroupNamesDapper()
        {
            var students = _studentRepository.ReadAll();
            var result = new List<string>();
            foreach (var student in students.Cast<Student>())
            {
                result.Add($"{student.Id} | {student.Name} | {student.Specialty} | {student.Group?.Name}");
            }
            return result;
        }
        public Dictionary<string, int> GetSpecialtyHistogram()
        {
            var students = _studentRepository.ReadAll();
            return students
                  .GroupBy(s => s.Specialty)
                  .ToDictionary(g => g.Key, g => g.Count());
        }
        public void FillTableGroups()
        {
            var groups = _groupRepository.ReadAll().ToList();
            if (groups.Count == 0)
            {

            }
        }
    }
}
