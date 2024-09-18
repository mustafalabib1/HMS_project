using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class MedicalRecord
    {
        public int RecordID { get; set; }

        public string Diagnosis { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string LabResults { get; set; }=null!;

        //public int PatientID { get; set; }
    }
}
