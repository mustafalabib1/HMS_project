using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HMS_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region how can get day in week 
            //DayOfWeek day = DateTime.Now.DayOfWeek;
            //Console.WriteLine($"Today is day number {day} of the week."); 
            #endregion
            using HMSdbcontext context = new HMSdbcontext();

            #region try add patient by use view model 
            //RegisterationViewModel PatViewModle = new RegisterationViewModel()
            //{
            //    SSN = 345678990,
            //    Address = "Mansoura",
            //    FirstName = "Musrtafa",
            //    MiddleName = "Labib",
            //    LastName = "Issa",
            //    Gender = Gender.Male,
            //    DateOfBirth = new DateOnly(2004, 2, 18),
            //    Email = "Mustafa@gmail.com"
            //    ,
            //    UserPassword = "1234567890",
            //    Phone = "1234567890"
            //};
            //Patient patient = (Patient)PatViewModle;
            //context.Patients.Add(patient);
            //context.SaveChanges();
            #endregion

            #region try show patient by use view model
            //var patient =context.Patients.ToList();
            //foreach (var item in patient)
            //{
            //    var pat = (RegisterationViewModel)item;
            //    Console.WriteLine($"{pat.SSN}:::{pat.FirstName}:::{pat.Phone}");
            //} 
            #endregion

            #region show mediction and active in each one 
            //var medicition=(context.Medication.Include(m=>m.ActiveSubstances)).Select(m => (MedicationViewModel)m).ToList();
            //foreach (var item in medicition)
            //{
            //    Console.WriteLine($"{item.MedName}  {item.Strength}");
            //    foreach (var item1 in item.ActSubInMed)
            //    {
            //        Console.WriteLine($"\t{item1.ActiveSubstancesName}");
            //    }
            //}
            //Console.WriteLine();
            //Console.WriteLine(); 
            #endregion

            #region view model of Activesubstance that show substance and list of interaction and medication 
            var ActSubs = (context.ActiveSubstances.Include(a => a.Medications).ToList()).Select(a => (ActiveSubstanceViewModel)a);
            foreach (var item in ActSubs)
            {
                Console.WriteLine("=============================================================================");
                Console.WriteLine($"{item.ActiveSubstancesName}");
                foreach (var item1 in item.Interactions)
                {
                    Console.WriteLine($"\t{item1.OtherSubstanceName}");
                }
                Console.WriteLine("-------------------------------------------------------");
                foreach (var item1 in item.Medications)
                {
                    Console.WriteLine($"\t{item1.MedName}");
                }
            }
            #endregion

            #region try add active substance by use view model
            //Creating new ViewModel
            //var viewModel = new ActiveSubstanceViewModel
            //{
            //    ActiveSubstancesName = "NewSubstance",
            //    Interactions = new HashSet<ActiveSubstanceInteractionViewModel>
            //    {
            //        new ActiveSubstanceInteractionViewModel
            //        {
            //            Interaction = "NewSubstance may interact with Acetaminophen",
            //            OtherSubstanceName = "Acetaminophen",
            //            ActSubId=1
            //        }
            //    }
            //};
            //context.ActiveSubstances.Add((ActiveSubstance)viewModel);
            //context.SaveChanges();
            #endregion
        }
    }
}
