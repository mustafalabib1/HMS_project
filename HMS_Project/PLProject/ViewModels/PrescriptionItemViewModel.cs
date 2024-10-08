using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class PrescriptionItemViewModel
    {
        public PrescriptionItemViewModel() { }

        public PrescriptionItemViewModel(PrescriptionItem prescriptionItem)
        {
            //Id = prescriptionItem.Id;
            FullDosage = prescriptionItem.FullDosage;
        }

        //[Required]
        //public int Id { get; set; }

        [Required]
        public string FullDosage { get; set; } = null!;

       
        public static explicit operator PrescriptionItem(PrescriptionItemViewModel viewModel)
        {
            return new PrescriptionItem()
            {
                //Id = viewModel.Id,
                FullDosage = viewModel.FullDosage
            };
        }

        
        public static explicit operator PrescriptionItemViewModel(PrescriptionItem prescriptionItem)
        {
            return new PrescriptionItemViewModel(prescriptionItem);
        }
    }
}
