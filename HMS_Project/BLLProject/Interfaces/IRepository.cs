using BLLProject.Specification;
using DALProject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Interfaces
{
    public interface IRepository <T> where T : ModelBase
    {
        public void Add(T entity);

        public void Update(T entity);

        public void Delete(T entity);

        public T Get(int Id);

        public IEnumerable<T> GetALL();

        public IQueryable<T> Find(Expression<Func<T, bool>> filter);
       

        public T GetEntityWithSpec(ISpecification<T> spec); 
        public IEnumerable<T> GetALLWithSpec(ISpecification<T> spec);
    }
}
