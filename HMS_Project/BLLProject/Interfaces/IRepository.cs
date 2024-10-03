using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Interfaces
{
    public interface IRepository <T> where T : class
    {
        public int Add(T entity);

        public int Update(T entity);

        public int Delete(T entity);

        public T Get(int Id);

        public IEnumerable<T> GetALL();
    }
}
