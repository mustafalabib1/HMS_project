using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using Microsoft.EntityFrameworkCore;
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

        public new Invoice Get(int Id)
        {
            var Invoices= context.Invoices.Include(r => r.Receptionist).Where(R => R.Id == Id).FirstOrDefault();
            return Invoices;
            
        }
    }
}
