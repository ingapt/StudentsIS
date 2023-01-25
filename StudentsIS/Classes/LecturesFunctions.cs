using StudentsIS.Entities;

namespace StudentsIS.Classes
{
	public static class LecturesFunctions
	{
		public static void CreateLecture(this StudentContext dbContext)
		{
			Console.Clear();
			Console.WriteLine("Įveskite paskaitos pavadinimą: ");
			var name = Console.ReadLine();
			Console.WriteLine("Įveskite kreditų skaičių");
			var creditOfLecture = Validation.GetValidIntergerNumber();
			Console.WriteLine("Įveskite dėstytoją. ");
			var teacher = Console.ReadLine();

			var newLecture = new Lecture()
			{
				Name = name,
				Credit = creditOfLecture,
				Teacher = teacher,
			};

			dbContext.Lectures.Add(newLecture);
			dbContext.SaveChanges();
			Console.WriteLine("Nauja paskaita sukurta. ");
			Console.ReadKey();
		}

		public static void DeleteLecture(this StudentContext dbContext)
		{ 
			Console.Clear();
			Dispay.DisplayLectures(dbContext);
			Console.WriteLine();
			Console.WriteLine("Pasirinkite paskaitos id, kurią reikia ištrinti");
			var input = Validation.GetValidIntergerNumber();

			var lecture = dbContext.Lectures.Where(x => x.Id == input).SingleOrDefault();
			dbContext.Lectures.Remove(lecture);
			dbContext.SaveChanges();
			Console.WriteLine("Paskaita ištrinta.");
			Console.ReadKey();
		}
	}
}
