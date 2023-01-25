using Microsoft.EntityFrameworkCore;
using StudentsIS.Classes.Functions;

namespace StudentsIS.Classes
{
    public static class DisplayStart
    {
        public static void Start(this StudentContext dbContext)
        {
			bool toDo = true;

			while (toDo)
			{
				Console.Clear();
				Console.WriteLine("Pasirinkite: ");
				Console.WriteLine("[1] Atvaizduoti visus departamento studentus \n[2] Atvaizduoti visas departamento paskaitas \n[3] Atvaizduotis visas paskaitas pagal studentą \n[4] Grįžti atgal");
				var input = Validation.GetValidNumbersFromConsole(4);
				switch (input)
				{
					case 1:
						Dispay.DisplayDepartamentsWithStudents(dbContext);
						break;
					case 2:
						Dispay.DisplayDepartamensWithLectures(dbContext);
						break;
					case 3:
						Dispay.DisplayStudentAndYourLectures(dbContext);
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
