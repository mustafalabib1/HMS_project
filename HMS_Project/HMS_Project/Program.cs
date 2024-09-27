using HMS_Project.Contexts;
using HMS_Project.model;

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

        }
    }
}
