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
			bool toDo = true;
			while (toDo)
			{
				Console.WriteLine("Pasirinkite: ");
				Console.WriteLine("[1] Sukurti naują studentą ir pridėti ją prie departamento. \n[2] Pridėti egzistuojantį studentą prie departamento \n[3] Grįžti atgal");
				var input = Validation.GetValidNumbersFromConsole(3);
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

		private static void InsertNewStudentToTheDepartament(StudentContext dbContext)
		{
			Console.Clear();
			Dispay.DisplayDepartaments(dbContext);
			Console.WriteLine("Sukurkite studentą: ");
			Console.WriteLine("Įveskite studento vardą: ");
			var studentName = Console.ReadLine();
			Console.WriteLine("Įveskite studento pavardę: ");
            var studentSurname = Console.ReadLine();
			
			var student = new Student(studentName, studentSurname);

			var numberOfDepartaments = dbContext.Departaments.Count();
			Console.WriteLine("Pasirinkite departamento id, į kurį įtrauksime paskaitą: ");
			var input = Validation.GetValidNumbersFromConsole(numberOfDepartaments);
			var departament = dbContext.Departaments.SingleOrDefault(x => x.Id == input);
            departament.Students.Add(student);
            
			dbContext.SaveChanges();
		    Console.WriteLine($"Sukurtas studentas ir priskirtas departamentui {departament.Name}");

		}

		private static void InsertExistingStudentToDepartament(StudentContext dbContext)
		{
			Console.Clear();
			var numberOfStudents = dbContext.Students.Count();
			Dispay.DisplayStudents(dbContext);
			Console.WriteLine("Pasirinkite studento id, kurį priskirsime departamentui. ");
			var intput_stud = Validation.GetValidNumbersFromConsole(numberOfStudents);
			var student = dbContext.Students.SingleOrDefault(x => x.Id == intput_stud);
			Console.WriteLine();

			var numberOfDepartaments = dbContext.Departaments.Count();
			Dispay.DisplayDepartaments(dbContext);
			Console.WriteLine("Pasirinkite departamento id, kuriam priskirsim studentą.");
			var input_dep = Validation.GetValidNumbersFromConsole(numberOfDepartaments);
            var departament = dbContext.Departaments.SingleOrDefault(x => x.Id == input_dep);

            student.DepartamentId = departament.Id; 
			dbContext.Students.Add(student);
			dbContext.SaveChanges();
			Console.WriteLine("Studentas priskirtas departamentui");
		}

		public static void InsertLectureToThisDeartament(this StudentContext dbContext, Departament departament)
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

        public static void InsertNewLectureToTheDepartament(StudentContext dbContext)
        {
            Console.Clear();
            Dispay.DisplayDepartaments(dbContext);
            Console.WriteLine("Sukurkite paskaitą: ");
            Console.WriteLine("Įveskite paskaitos pavadinimą: ");
            var lectureName = Console.ReadLine();
            Console.WriteLine("Įveskite kreditų skaičių: ");
            var creditOfLecture = Validation.GetValidIntergerNumber();
            Console.WriteLine("Įveskite dėstytojo pavardę: ");
            var teacherOfLecture = Console.ReadLine();
            var lecture = new Lecture(lectureName, creditOfLecture, teacherOfLecture);
            
            var numberOfDepartaments = dbContext.Departaments.Count();
            Console.WriteLine("Pasirinkite departamento id, į kurį įtrauksime paskaitą: ");
            var input = Validation.GetValidNumbersFromConsole(numberOfDepartaments);
            var departament = dbContext.Departaments.SingleOrDefault(x => x.Id == input);
            lecture.Departaments.Add(dbContext.Departaments.Where(x => x.Id == input).SingleOrDefault());
            dbContext.Lectures.Add(lecture);
            dbContext.SaveChanges();
            Console.WriteLine($"Paskaita sukurta ir priskirta departamentui {departament.Name}");
        }

        public static void InsertExistingLectureToDepartament(StudentContext dbContext)
        {
            Console.Clear();
            var numberofLectures = dbContext.Lectures.Count();
            Dispay.DisplayLectures(dbContext);
            Console.WriteLine("Pasirinkite paskaitos id, kurią priskirsime departamentui. ");
            var intput_lec = Validation.GetValidNumbersFromConsole(numberofLectures);
            var lecture = dbContext.Lectures.SingleOrDefault(x=> x.Id == intput_lec);
            Console.WriteLine();

            var numberOfDepartaments = dbContext.Departaments.Count();
            Dispay.DisplayDepartaments(dbContext);
            Console.WriteLine("Pasirinkite departamento id, į kurį priskirsim paskaitą.");
            var input_dep = Validation.GetValidNumbersFromConsole(numberOfDepartaments);

            lecture.Departaments.Add(dbContext.Departaments.Where(x => x.Id == input_dep).SingleOrDefault());
            dbContext.Lectures.Add(lecture);
            dbContext.SaveChanges();
            Console.WriteLine("Paskaita priskirta departamentui.");

        }
    }
}

