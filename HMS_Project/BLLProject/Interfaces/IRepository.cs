﻿using DALProject.model;
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
        public int Add(T entity);

        public int Update(T entity);

        public int Delete(T entity);

        public T Get(int Id);

        public IEnumerable<T> GetALL();

        public IQueryable<T> Find(Expression<Func<T, bool>> filter);
    }
}
