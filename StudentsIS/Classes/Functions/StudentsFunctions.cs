using Microsoft.EntityFrameworkCore;
using StudentsIS.Entities;

namespace StudentsIS.Classes.Functions
{
    public static class StudentsFunctions
    {
        public static void AddLecturesForStudent(this StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayStudents();
            Console.WriteLine("Įveskite studento id.: ");
            var input = Validation.GetValidIntergerNumber();
            var student = dbContext.Students.Where(x => x.Id == input).SingleOrDefault();
            
            var lectures = dbContext.Departaments.Include("Lectures").Where(x => x.Id == student.DepartamentId).First().Lectures.ToList();
            foreach (var lecture in lectures)
            { 
                student.Lectures.Add(lecture);
            }
                  
            dbContext.SaveChanges();
            Console.WriteLine("Studentui įtrauktos paskaitos.");
            Console.ReadKey();
        }

        public static void ChangeDepartamentAndLecturesForStudent(this StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayStudents();
            Console.WriteLine("Pairinkite studento Id, kuriam pakeisime departamentą. ");
            var input_studId = Validation.GetValidIntergerNumber();
            var student = dbContext.Students.Where(x => x.Id == input_studId).SingleOrDefault();

            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, į kurį perkelsime studentą");
            var input_depId = Validation.GetValidIntergerNumber();
            student.DepartamentId = input_depId;
            student.Lectures.Clear();
			var lectures = dbContext.Departaments.Include("Lectures").Where(x => x.Id == student.DepartamentId).First().Lectures.ToList();
			foreach (var lecture in lectures)
			{
				student.Lectures.Add(lecture);
			}
			dbContext.SaveChanges();
            Console.WriteLine("Studentui pakeistas departamentas ir paskaitos");
            Console.ReadKey();
        }

        public static void DeleteStudent(this StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayStudents();
            Console.WriteLine("Pasirinkite studento id, kurį pašalinsime.: ");
            var input_stud = Validation.GetValidIntergerNumber();
            var student = dbContext.Students.Include("Lectures").Where(x => x.Id == input_stud).SingleOrDefault();
            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
            Console.WriteLine("Studentas pašalintas. ");
            Console.ReadKey();
        }
    }
}
