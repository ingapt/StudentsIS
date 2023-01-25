using Microsoft.EntityFrameworkCore;
using StudentsIS.Entities;

namespace StudentsIS.Classes.Functions
{
    public static class StudentsFunctions
    {
        public static void CreateStudent(this StudentContext dbContext)
        {
            Console.Clear();
            Console.WriteLine("Įveskite studento vardą: ");
            var name = Console.ReadLine();
            Console.WriteLine("Įveskite studento pavardę");
            var surname = Console.ReadLine();

            var newStudent = new Student()
            {
                Name = name,
                Surname = surname,
            };

            dbContext.Students.Add(newStudent);
            dbContext.SaveChanges();
            Console.WriteLine("Naujas studentas įvestas");
            Console.ReadKey();
        }

        public static void AddLecturesForStudent(this StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayStudents();
            Console.WriteLine("Įveskite studento id.: ");
            var input = Validation.GetValidIntergerNumber();
            var student = dbContext.Students.Where(x => x.Id == input).SingleOrDefault();
            dbContext.AddLectures(student);
            dbContext.SaveChanges();
            Console.WriteLine("Studentas įtrauktos paskaitos.");
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
            dbContext.AddLectures(student);
            dbContext.SaveChanges();
            Console.WriteLine("Studentui pakeistas departamentas ir paskaitos");
        }

        public static void AddLectures(this StudentContext dbContext, Student student)
        {
            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, kuriam priklausys studentas");
            var input_dep = Validation.GetValidIntergerNumber();
            var lectures = dbContext.Departaments.Include("Lectures").Where(x => x.Id == input_dep).SingleOrDefault().Lectures.ToList();

            foreach (var lecuture in lectures)
            {
                var lectureId = lecuture.Id;
                student.Lectures.Add(dbContext.Lectures.Where(x => x.Id == lectureId).SingleOrDefault());
            }
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
        }
    }
}
