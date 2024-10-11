using DALProject.model;

namespace PLProject.ViewModels.Presciption
{
    public static class PrescriptionHelper
    {
        public static Prescription ConvertPrescriptionViewModelToPresciption(this Prescription prescription, PrescriptionViewModel ViewModel)
        {
            prescription.DoctorId = ViewModel.DoctorId;
            prescription.PrescriptionItems = ViewModel.PrescriptionItems.Select(i => new PrescriptionItem().ConvertPrescriptionItemDoctorVMToPrescriptionItem(i)).ToList();
            return prescription;
        }
        public static PrescriptionViewModel ConvertPresciptionToPrescriptionViewModel(this Prescription prescription)
        {
            return new PrescriptionViewModel()
            {
                PrescriptionItems = prescription.PrescriptionItems.Select(i => i.ConvertPrescriptionItemToPrescriptionItemDoctorVM()).ToList(),
                DoctorId = prescription.DoctorId,
                Patient = prescription.Apointment.Patient,
                prescriptionId = prescription.Id
            };
        }
        public static PrescriptionItem ConvertPrescriptionItemDoctorVMToPrescriptionItem(this PrescriptionItem prescriptionItem, PrescriptionItemDoctorVM ViewModel)
        {
            prescriptionItem.FullDosage = ViewModel.FullDosage;
            prescriptionItem.ActiveSubstanceId = ViewModel.ActiveSubstanceId;
            return prescriptionItem;
        }
        public static PrescriptionItemDoctorVM ConvertPrescriptionItemToPrescriptionItemDoctorVM(this PrescriptionItem item)
        {
            return new PrescriptionItemDoctorVM()
            {
                ActiveSubstanceId = item.ActiveSubstance.Id,
                FullDosage = item.FullDosage,
            };
        }
    }
}
