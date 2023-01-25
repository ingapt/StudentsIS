﻿using Azure.Identity;
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
            var numberOfDepartaments = dbContext.Departaments.Count();
            Console.WriteLine("Pasirinkite departamento id, į kurį įtrauksime paskaitą: ");
            var input = numberOfDepartaments.GetValidNumbersFromConsole();
            input = input + 4;
            Console.WriteLine("Sukurkite studentą: ");
            Console.WriteLine("Įveskite studento vardą: ");
            var studentName = Console.ReadLine();
            Console.WriteLine("Įveskite studento pavardę: ");
            var studentSurname = Console.ReadLine();

            var student = new Student(studentName, studentSurname);

            var departament = dbContext.Departaments.Include("Students").Where(x => x.Id == input).SingleOrDefault();
            student.DepartamentId = input;
            departament.Students.Add(student);
            dbContext.SaveChanges();
            Console.WriteLine($"Sukurtas studentas ir priskirtas departamentui {departament.Name}");
            Console.ReadKey();
        }

        public static void InsertExistingStudentToDepartament(StudentContext dbContext)
        {
            Console.Clear();
            var numberOfStudents = dbContext.Students.Count();
            dbContext.DisplayStudents();
            Console.WriteLine("Pasirinkite studento id, kurį priskirsime departamentui. ");
            var intput_stud = numberOfStudents.GetValidNumbersFromConsole();
            var student = dbContext.Students.SingleOrDefault(x => x.Id == intput_stud);
            Console.WriteLine();

            var numberOfDepartaments = dbContext.Departaments.Count();
            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, kuriam priskirsim studentą.");
            var input_dep = numberOfDepartaments.GetValidNumbersFromConsole();
            var departament = dbContext.Departaments.SingleOrDefault(x => x.Id == input_dep);

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
            var numberOfDepartaments = dbContext.Departaments.Count();
            Console.WriteLine("Pasirinkite departamento id, į kurį įtrauksime paskaitą: ");
            var input = numberOfDepartaments.GetValidNumbersFromConsole();
            input = input + 4;
            Console.WriteLine();
            Console.WriteLine("Sukurkite paskaitą: ");
            Console.WriteLine("Įveskite paskaitos pavadinimą: ");
            var lectureName = Console.ReadLine();
            Console.WriteLine("Įveskite kreditų skaičių: ");
            var creditOfLecture = Validation.GetValidIntergerNumber();
            Console.WriteLine("Įveskite dėstytojo pavardę: ");
            var teacherOfLecture = Console.ReadLine();
            var lecture = new Lecture(lectureName, creditOfLecture, teacherOfLecture);

            var departament = dbContext.Departaments.Include("Lectures").Where(x => x.Id == input).SingleOrDefault();
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

            var numberOfDepartaments = dbContext.Departaments.Count();
            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, į kurį priskirsim paskaitą.");
            var input_dep = numberOfDepartaments.GetValidNumbersFromConsole();
            input_dep = input_dep + 4;  //Mano duomenų bazėj departamento Id prasideda nuo 5. 
            var departament = dbContext.Departaments.Include("Lectures").Where(x => x.Id == input_dep).SingleOrDefault();
            departament.Lectures.Add(lecture);

            dbContext.SaveChanges();
            Console.WriteLine("Paskaita priskirta departamentui.");
            Console.ReadKey();
        }

        public static void DeleteDepratament(this StudentContext dbContext)
        {
            Console.Clear();
            var numberOfDepart = dbContext.Departaments.Count();
            dbContext.DisplayDepartaments();
            Console.WriteLine("Pasirinkite departamento id, kurį pašalinsime.: ");
            var input_dep = numberOfDepart.GetValidNumbersFromConsole();
            var departament = dbContext.Departaments.Include("Students").Where(x => x.Id == input_dep).SingleOrDefault();
            var students = departament.Students;
            dbContext.Students.RemoveRange(students);
            dbContext.Departaments.Remove(departament);
            dbContext.SaveChanges();
            Console.WriteLine("Departamentas pašalintas. ");
        }
    }
}
