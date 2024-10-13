using BLLProject.Repositories;
using DALProject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<T> Repository<T>() where T : ModelBase;

        int Complete();
    }
}
