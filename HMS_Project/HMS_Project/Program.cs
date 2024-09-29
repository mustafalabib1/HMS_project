using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;

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

            var patient =context.Patients.ToList();
            foreach (var item in patient)
            {
                var pat = (RegisterationViewModel)item;
                Console.WriteLine($"{pat.SSN}:::{pat.FirstName}:::{pat.Phone}");
            }
        }
    }
}
