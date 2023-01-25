using StudentsIS.Entities;
using System.Runtime.CompilerServices;

namespace StudentsIS.Classes
{
    public static class Dispay
    {
        public static void DisplayDepartaments(this StudentContext dbContext)
        {
            var i = 1;
            var departaments = dbContext.Departaments.ToList();
            
            foreach (var departament in departaments)
            {
                Console.WriteLine($"{i++} {departament.Id} {departament.Name}");
            }
        }

        public static void DisplayLectures(this StudentContext dbContext)
        { 
            var lectures = dbContext.Lectures.ToList();

            foreach(var lecture in lectures) 
            {
                Console.WriteLine($"{lecture.Id} {lecture.Name}");
            }
        }

		public static void DisplayStudents(this StudentContext dbContext)
		{
			var students = dbContext.Students.ToList();

			foreach (var student in students)
			{
				Console.WriteLine($"{student.Id} {student.Name} {student.Surname}");
			}
		}
	}
}
