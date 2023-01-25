using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using StudentsIS.Classes;
using StudentsIS.Entities;
using System.Security.Cryptography.X509Certificates;

namespace StudentsIS.Classes
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

            Console.WriteLine("Ar norite pridėti studentų? Taip - t, T; Ne - n, N");
            var input = Validation.GetYesOrNoFromConsole();
            if (input == 'T')
            {
                InsertStudentToThisDeartament(dbContext, newDepartament.Id);
            }

            Console.WriteLine("Ar norite pridėti paskaitų? Taip - t, T; Ne - n, N");
            input = Validation.GetYesOrNoFromConsole();
            if (input == 'T')
            {
                InsertLectureToThisDeartament(dbContext, newDepartament);
            }

            dbContext.SaveChanges();

        }

        public static void InsertLectureToDepartament(this StudentContext dbContext)
        {
            bool toDo = true;
            while (toDo)
            {
                Console.WriteLine("Pasirinkite: ");
                Console.WriteLine("[1] Sukurti naują paskaitą ir pridėti ją prie departamento. \n[2] Pridėti egzistuojančią paskaitą prie departamento \n[3] Grįžti atgal");
                var input = Validation.GetValidNumbersFromConsole(3);
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

        }

        private static void InsertStudentToThisDeartament(this StudentContext dbContext, Guid departamentId)
        {
            bool toDo = true;
            while (toDo)
            {
                Console.Clear();
                Console.WriteLine("Įveskite studento vardą: ");
                var name = Console.ReadLine();
                Console.WriteLine("Įveskite studento pavardę: ");
                var surname = Console.ReadLine();
                dbContext.Students.Add(new Student(name, surname, departamentId));

                Console.WriteLine("Ar norite dar įtraukti studentą? Taip - T, t; Ne - N, n");
                var input = Validation.GetYesOrNoFromConsole();
                if (input == 'N')
                {
                    toDo = false;
                }
            }
            dbContext.SaveChanges();
        }

        private static void InsertLectureToThisDeartament(this StudentContext dbContext, Departament departament)
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
                

                Console.WriteLine("Ar norite dar įtraukti paskaitą? Taip - T, t; Ne - N, n");
                var input = Validation.GetYesOrNoFromConsole();
                if (input == 'N')
                {
                    toDo = false;
                }
                dbContext.SaveChanges();
            }
        }

        private static void InsertNewLectureToTheDepartament(StudentContext dbContext)
        {
            Console.Clear();
            Dispay.DisplayDepartaments(dbContext);
            Console.WriteLine("Pasirinkite departamento pavadinimą, į kurį įtrauksime paskaitą: ");
            var name = Console.ReadLine();

            var departament = dbContext.Departaments.Where(x => x.Name == name).SingleOrDefault();
            if (departament != null)
            {
                var departamentId = departament.Id;
                Console.WriteLine("Įveskite paskaitos pavadinimą: ");
                name = Console.ReadLine();
                Console.WriteLine("Įveskite kreditų skaičių: ");
                var credit = Validation.GetValidIntergerNumber();
                Console.WriteLine("Įveskite dėstytojo pavardę");
                var teacher = Console.ReadLine();

                var lecture = new Lecture(name, credit, teacher);
                

                dbContext.SaveChanges();
            }
        }

        private static void InsertExistingLectureToDepartament(StudentContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}

