using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repositorys
{
    class TeacherRepository : Repository<Teacher>
    {
        protected override string FilePath => "TeachersDB.db";

        protected override bool IsIgnored(string propName)
        {
            switch (propName)
            {
                case nameof(Teacher.FullName): return true;
                default: return false;
            }
        }
    }
}
