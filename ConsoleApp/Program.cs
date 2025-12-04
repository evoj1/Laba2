using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<Logic>();

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Показать всех студентов");
                Console.WriteLine("2 - Добавить студента");
                Console.WriteLine("3 - Удалить студента");
                Console.WriteLine("4 - Показать гистограмму");
                Console.WriteLine("5 - Показать все группы");
                Console.WriteLine("6 - Добавиить группу");
                Console.WriteLine("7 - Удалить группу");
                Console.WriteLine("8 - Выйти");
                Console.Write("Действие: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowStudents(logic.PrintAllStudentsWithGroupNames());
                        //ShowStudents(logic.PrintAllStudentsWithGroupNamesDapper());
                        break;
                    case "2":
                        AddStudent(logic);
                        break;
                    case "3":
                        DeleteStudent(logic);
                        break;
                    case "4":
                        ShowHistogram(logic);
                        break;
                    case "5":
                        ShowGroups(logic);
                        break;
                    case "6":
                        AddGroup(logic);
                        break;
                    case "7":
                        DeleteGroup(logic);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Введите корректное действие");
                        break;
                }
            }
        }

        static void ShowStudents(List<string> students)
        {
            Console.WriteLine("\nСтуденты в базе:");
            foreach (var s in students)
                Console.WriteLine($"{s}");
        }

        static void ShowGroups(Logic logic)
        {
            var groups = logic.GetAllGroups();
            Console.WriteLine("\n" + new string('─', 40));
            Console.WriteLine("Список групп:");
            Console.WriteLine(new string('─', 40));
            if (groups.Count == 0)
            {
                Console.WriteLine("Нет групп в базе данных.");
            }
            else
            {
                foreach (var g in groups)
                    Console.WriteLine($"  {g}");
            }
            Console.WriteLine(new string('─', 40));
        }

        static void AddStudent(Logic logic)
        {
            Console.Write("Имя: ");
            string name = Console.ReadLine();

            Console.Write("Специальность: ");
            string specialty = Console.ReadLine();

            var groups = logic.GetAllGroups();
            Console.WriteLine("Группы:");
            foreach (var g in groups)
            {
                Console.WriteLine(g);
            }
            Console.Write("Выберите группу: ");
            int.TryParse(Console.ReadLine(), out int groupId);

            logic.AddStudent(name, specialty, groupId);
            Console.WriteLine("Студент добавлен");

            ShowStudents(logic.PrintAllStudentsWithGroupNames());
        }
        static void AddGroup(Logic logic)
        {
            Console.Write("Имя: ");
            string name = Console.ReadLine();

            logic.AddGroup(name);
            Console.WriteLine("Группа добавлена");

            var groups = logic.GetAllGroups();
            Console.WriteLine("Группы:");
            foreach (var g in groups)
            {
                Console.WriteLine(g);
            }
        }
        static void DeleteStudent(Logic logic)
        {
            Console.Write("Id студента для удаления: ");
            int.TryParse(Console.ReadLine(), out int id);
            if (logic.RemoveStudent(id))
            {
                Console.WriteLine("Студент удалён");
                ShowStudents(logic.PrintAllStudentsWithGroupNames());
            }
            else
                Console.WriteLine("Студент не найден");
        }
        static void DeleteGroup(Logic logic)
        {
            var groups = logic.GetAllGroups();
            Console.WriteLine("Группы:");
            foreach (var g in groups)
            {
                Console.WriteLine(g);
            }

            Console.Write("Id группы для удаления: ");
            int.TryParse(Console.ReadLine(), out int id);
            if (logic.RemoveGroup(id))
                Console.WriteLine("Группа удалена");
            else
                Console.WriteLine("Группа не найдена");
        }
        static void ShowHistogram(Logic logic)
        {
            var hist = logic.GetSpecialtyHistogram();
            Console.WriteLine("\nГистограмма по специальности:");
            foreach (var pair in hist)
            {
                string bar = new string('█', pair.Value);
                Console.WriteLine($"{pair.Key,-10} | {bar,-15} {pair.Value}");
            }
        }
    }
}
