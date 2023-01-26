using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using StudentsIS.Entities;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace StudentsIS.Classes.Functions
{
    public static class DepartamentsFunctions
    {
        public static void CreateDepartament(this StudentContext dbContext)
        {
            Console.Clear();
            Console.WriteLine("Įveskite departamento pavadinimą: ");
            var name = Console.ReadLine();
            var newDepartament = new Departament()
            {
                Name = name,
            };
            dbContext.Departaments.Add(newDepartament);
            dbContext.SaveChanges();

            Console.WriteLine("Ar norite pridėti studentų? Taip - t, T; Ne - n, N");
            var input = Validation.GetYesOrNoFromConsole();
            if (input == 'T')
            {
                dbContext.InsertStudentToThisDeartament(newDepartament.Id);
            }

            Console.WriteLine("Ar norite pridėti paskaitų? Taip - t, T; Ne - n, N");
            input = Validation.GetYesOrNoFromConsole();
            if (input == 'T')
            {
                dbContext.InsertLectureToThisDeartament(newDepartament.Id);
            }
        }

        public static void InsertLectureToDepartament(this StudentContext dbContext)
        {
            bool toDo = true;
            while (toDo)
            {
                Console.Clear();
                Console.WriteLine("Pasirinkite: ");
                Console.WriteLine("[1] Sukurti naują paskaitą ir pridėti ją prie departamento. \n[2] Pridėti egzistuojančią paskaitą prie departamento \n[3] Grįžti atgal");
                var input = 3.GetValidNumbersFromConsole();
                switch (input)
                {
                    case 1:
                        InsertNewLectureToTheDepartament(dbContext);
                        break;
                    case 2:
                        InsertExistingLectureToDepartament(dbContext);
                        break;
                    case 3:
                        toDo = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void InsertStudentToDepartament(this StudentContext dbContext)
        {
            bool toDo = true;
            while (toDo)
            {
                Console.Clear();
                Console.WriteLine("Pasirinkite: ");
                Console.WriteLine("[1] Sukurti naują studentą ir pridėti ją prie departamento. \n[2] Pridėti egzistuojantį studentą prie departamento \n[3] Grįžti atgal");
                var input = 3.GetValidNumbersFromConsole();
                switch (input)
                {
                    case 1:
                        InsertNewStudentToTheDepartament(dbContext);
                        break;
                    case 2:
                        InsertExistingStudentToDepartament(dbContext);
                        break;
                    case 3:
                        toDo = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void InsertStudentToThisDeartament(this StudentContext dbContext, int departamentId)
        {
            bool toDo = true;
            while (toDo)
            {
                Console.Clear();
                Console.WriteLine("Įveskite studento vardą: ");
                var name = Console.ReadLine();
                Console.WriteLine("Įveskite studento pavardę: ");
                var surname = Console.ReadLine();

                var departament = dbContext.Departaments.Include("Students").Where(x => x.Id == departamentId).SingleOrDefault();
                var student = new Student(name, surname, departamentId);
                departament.Students.Add(student);
                dbContext.SaveChanges();

                Console.WriteLine("Ar norite dar įtraukti studentą? Taip - T, t; Ne - N, n");
                var input = Validation.GetYesOrNoFromConsole();
                if (input == 'N')
                {
                    toDo = false;
                }
            }
        }

        private static void InsertNewStudentToTheDepartament(StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, į kurį įtrauksime paskaitą: ");
            var input_depId = Validation.GetValidIntergerNumber();
            
            Console.WriteLine("Sukurkite studentą: ");
            Console.WriteLine("Įveskite studento vardą: ");
            var studentName = Console.ReadLine();
            Console.WriteLine("Įveskite studento pavardę: ");
            var studentSurname = Console.ReadLine();
            var student = new Student(studentName, studentSurname);
            var departament = dbContext.Departaments.Include("Students").Where(x => x.Id == input_depId).SingleOrDefault();
            student.DepartamentId = input_depId;
            departament.Students.Add(student);
            dbContext.SaveChanges();
            Console.WriteLine($"Sukurtas studentas ir priskirtas departamentui {departament.Name}");
            Console.ReadKey();
        }

        public static void InsertExistingStudentToDepartament(StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayStudents();
            Console.WriteLine("Pasirinkite studento id, kurį priskirsime departamentui. ");
            var intput_studId = Validation.GetValidIntergerNumber();
            var student = dbContext.Students.SingleOrDefault(x => x.Id == intput_studId);
            Console.WriteLine();

            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, kuriam priskirsim studentą.");
            var input_depId = Validation.GetValidIntergerNumber();
            var departament = dbContext.Departaments.SingleOrDefault(x => x.Id == input_depId);

            student.DepartamentId = departament.Id;
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
            Console.WriteLine("Studentas priskirtas departamentui");
            Console.ReadKey();
        }

        public static void InsertLectureToThisDeartament(this StudentContext dbContext, int departamentId)
        {
            bool toDo = true;

            while (toDo)
            {
                Console.Clear();
                Console.WriteLine("Įveskite paskaitos pavadinimą: ");
                var name = Console.ReadLine();
                Console.WriteLine("Įveskite kreditų skaičių:");
                var credit = Validation.GetValidIntergerNumber();
                Console.WriteLine("Įveskite dėstytojo pavardę. ");
                var teacher = Console.ReadLine();
                var lecture = new Lecture(name, credit, teacher);

                var departament = dbContext.Departaments.Include("Lectures").Where(x => x.Id == departamentId).SingleOrDefault();
                departament.Lectures.Add(lecture);
                dbContext.SaveChanges();
                Console.WriteLine();
                Console.WriteLine("Ar norite dar įtraukti paskaitą? Taip - T, t; Ne - N, n");
                var input = Validation.GetYesOrNoFromConsole();
                if (input == 'N')
                {
                    toDo = false;
                }
            }
        }

        public static void InsertNewLectureToTheDepartament(StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, į kurį įtrauksime paskaitą: ");
            var input_depId = Validation.GetValidIntergerNumber();
            Console.WriteLine();
            Console.WriteLine("Sukurkite paskaitą: ");
            Console.WriteLine("Įveskite paskaitos pavadinimą: ");
            var lectureName = Console.ReadLine();
            Console.WriteLine("Įveskite kreditų skaičių: ");
            var creditOfLecture = Validation.GetValidIntergerNumber();
            Console.WriteLine("Įveskite dėstytojo pavardę: ");
            var teacherOfLecture = Console.ReadLine();
            var lecture = new Lecture(lectureName, creditOfLecture, teacherOfLecture);

            var departament = dbContext.Departaments.Include("Lectures").Where(x => x.Id == input_depId).SingleOrDefault();
            departament.Lectures.Add(lecture);
            dbContext.SaveChanges();
            Console.WriteLine($"Paskaita sukurta ir priskirta departamentui {departament.Name}");
            Console.ReadKey();
        }

        public static void InsertExistingLectureToDepartament(StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayLectures();
            Console.WriteLine("Pasirinkite paskaitos id, kurią priskirsime departamentui. ");
            var intput_lec = Validation.GetValidIntergerNumber();
            var lecture = dbContext.Lectures.SingleOrDefault(x => x.Id == intput_lec);
            Console.WriteLine();

            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, į kurį priskirsim paskaitą.");
            var input_depId = Validation.GetValidIntergerNumber();
            var departament = dbContext.Departaments.Include("Lectures").Where(x => x.Id == input_depId).SingleOrDefault();
            departament.Lectures.Add(lecture);

            dbContext.SaveChanges();
            Console.WriteLine("Paskaita priskirta departamentui.");
            Console.ReadKey();
        }

        public static void DeleteDepratament(this StudentContext dbContext)
        {
            Console.Clear();
            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, kurį pašalinsime.: ");
            var input_depId = Validation.GetValidIntergerNumber();
            var departament = dbContext.Departaments.Include("Students").Where(x => x.Id == input_depId).SingleOrDefault();
            var students = departament.Students;
            dbContext.Students.RemoveRange(students);
            dbContext.Departaments.Remove(departament);
            dbContext.SaveChanges();
            Console.WriteLine("Departamentas pašalintas. ");
        }
    }
}

