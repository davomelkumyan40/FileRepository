using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Models
{
    class Teacher
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public byte Age { get; set; }
        public University University { get; set; }
        public string FullName => $"{Name} {SurName}";

        public override string ToString()
        {
            return FullName.ToString();
        }
    }
}
