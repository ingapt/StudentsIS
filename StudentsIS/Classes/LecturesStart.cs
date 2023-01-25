using Microsoft.EntityFrameworkCore;
using StudentsIS.Classes.Functions;

namespace StudentsIS.Classes
{
    public static class LecturesStart
    {
        public static void Start(this StudentContext dbContext)
        {
			bool toDo = true;

			while (toDo)
			{
				Console.Clear();
				Console.WriteLine("Pasirinkite: ");
				Console.WriteLine("[1] Sukurti paskaitą \n[2] Įterpti paskaitą į departamentą \n[3] Ištrinti paskaitą \n[4] Grįžti atgal");
				var input = Validation.GetValidNumbersFromConsole(4);
				switch (input)
				{
					case 1:
						LecturesFunctions.CreateLecture(dbContext);
						break;
					case 2:
						DepartamentsFunctions.InsertExistingLectureToDepartament(dbContext);
						break;
					case 3:
						LecturesFunctions.DeleteLecture(dbContext);
						break;
					case 4:
						toDo = false;
						break;
					default:
						break;
				}

			}
		}
    }
}
