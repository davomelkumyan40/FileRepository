using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo.Repositorys;

namespace Repo
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = CreateStudents(10).ToList();
            Repository<Student> repo = new StudentRepository();
            List<Student> studs = repo.AsEnumerable().ToList();
            repo.AddRange(list);
            repo.Save();
        }

        public static IEnumerable<Student> CreateStudents(int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                yield return new Student
                {
                    Name = $"A{i}",
                    SurName = $"A{i}yan",
                    Age = (byte)rnd.Next(18, 50),
                    University = (University)rnd.Next(0, 4)
                };
            }
        }
    }
}
