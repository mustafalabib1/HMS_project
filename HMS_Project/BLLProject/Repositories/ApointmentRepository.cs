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
    internal class ApointmentRepository : Repository<Apointment>, IApointmentRepository
    {
        private readonly HMSdbcontext context;

        public ApointmentRepository(HMSdbcontext context) : base(context)
        {
            this.context = context;
        }
    }
}
