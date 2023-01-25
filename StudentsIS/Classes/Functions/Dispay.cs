using Microsoft.EntityFrameworkCore;
using StudentsIS.Entities;
using System.Runtime.CompilerServices;

namespace StudentsIS.Classes.Functions
{
    public static class Dispay
    {
        public static void DisplayStudentAndYourLectures(this StudentContext dbContext)
        {
            Console.Clear();
            Dispay.DisplayStudents(dbContext);
            Console.WriteLine("Pasirinkite studento id. Atvaizduosime visas jo paskaitas.");
            var input_studId = Validation.GetValidIntergerNumber();
			var student = dbContext.Students.Include("Lectures").Where(x => x.Id == input_studId).SingleOrDefault();
            if (student != null)
            {
                Console.WriteLine($"Studentas: {student.Id} {student.Name} {student.Surname}");
                if (student.Lectures.Count > 0)
                {
                    Console.WriteLine("Studento paskaitos: ");
                    foreach (var lecture in student.Lectures)
                    {
                        Console.WriteLine($"{lecture.Name} {lecture.Credit} {lecture.Teacher}");
                    }
                }
                else
                {
                    Console.WriteLine("Šis studentas neturi priskirtų jam paskaitų. ");
                }
            }
            else
            { 
                Console.WriteLine("Toks studentas neegzistuoja");
            }
		}

        public static void DisplayDepartamensWithLectures(this StudentContext dbContext)
        {
            Console.Clear();
            Dispay.DisplayDepartaments(dbContext);
            Console.WriteLine("Pasirinkite departamento Id. Atvaizduosimo jo paskaitas");
            var input_lectId = Validation.GetValidIntergerNumber();

        }

        public static void DisplayDepartaments(this StudentContext dbContext)
        {
            var i = 1;
            var departaments = dbContext.Departaments.ToList();

            foreach (var departament in departaments)
            {
                Console.WriteLine($"{i++} {departament.Name}");
            }
        }

        public static void DisplayLectures(this StudentContext dbContext)
        {
            var lectures = dbContext.Lectures.ToList();

            foreach (var lecture in lectures)
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
