using BLLProject.Interfaces;
using BLLProject.Specification;
using DALProject.Data.Contexts;
using DALProject.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Repositories
{
    public class Repository<T> : IRepository<T> where T : ModelBase
    {
        private readonly HMSdbcontext context;

        public Repository(HMSdbcontext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
          
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            
        }
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
           
        }

        public T Get(int Id) => context.Set<T>().Find(Id);

        public IEnumerable<T> GetALL() => context.Set<T>().AsNoTracking().ToList();

        public IQueryable<T> Find(Expression<Func<T, bool>> filter)
        {
           return context.Set<T>().Where(filter);
        }

        public T GetEntityWithSpec(ISpecification<T> spec) => ApplySpec(spec).FirstOrDefault();

        public IEnumerable<T> GetALLWithSpec(ISpecification<T> spec) => ApplySpec(spec).AsNoTracking().ToList();


        //helper
        private IQueryable<T> ApplySpec(ISpecification<T> spec) => SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);


    }
}
