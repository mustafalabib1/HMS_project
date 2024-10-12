using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HMSdbcontext dbcontext;

        private Hashtable _repsitories;
        public UnitOfWork(HMSdbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
            _repsitories = new Hashtable();


        }
        public Repository<T> Repository<T>() where T : ModelBase
        {
            var Key = typeof(T).Name;
            if (!_repsitories.ContainsKey(Key))
            {
                var repo = new Repository<T>(dbcontext);
                _repsitories.Add(Key, repo);
            }
            return _repsitories[Key] as Repository<T>;
        }


        public int Complete()
        {
           return dbcontext.SaveChanges();
            
        }

        public void Dispose()
        {
            dbcontext.Dispose();
        }

    
    }
}
