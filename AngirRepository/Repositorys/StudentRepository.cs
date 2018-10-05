using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repositorys
{
    class StudentRepository : Repository<Student>
    {
        protected override string FilePath => "StudentsDB.db";

        protected override bool IsIgnored(string propName)
        {
            switch (propName)
            {
                case nameof(Student.FullName): return true;
                default: return false;
            }
        }
    }
}
