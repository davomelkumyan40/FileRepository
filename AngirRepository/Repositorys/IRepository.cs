using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repositorys
{
    interface IRepository<T>
    {
        void Add(T model);
        void Insert(int index, T model);
        void Save();
        void AddRange(IEnumerable<T> collection);
        IEnumerable<T> AsEnumerable();
    }
}
