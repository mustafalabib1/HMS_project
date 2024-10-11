using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private readonly HMSdbcontext context;

        public InvoiceRepository(HMSdbcontext context) : base(context)
        {
            this.context = context;
        }
    }
}
