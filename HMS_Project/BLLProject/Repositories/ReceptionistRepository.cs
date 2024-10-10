using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Repositories
{
    internal class ReceptionistRepository : Repository<Invoice> , IReceptionistRepository
    {
        private readonly HMSdbcontext context;

        public ReceptionistRepository(HMSdbcontext context) : base(context)
        {
            this.context = context;
        }

       
    }
}
