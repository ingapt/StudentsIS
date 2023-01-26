
using StudentsIS.Classes.Functions;

namespace StudentsIS.Classes
{
    public static class StudentsStart
	{
		public static void Start(this StudentContext dbContext)
		{
			bool toDo = true;

			while (toDo)
			{
				Console.Clear();
				Console.WriteLine("Pasirinkite: ");
				Console.WriteLine("[1] Priskirti studentui paskaitas \n[2] Perkelti studentą į kitą departamentą ir pakeisti jo paskaitas  \n[3] Ištrinti studentą \n[4] Grįžti atgal");
				var input = Validation.GetValidNumbersFromConsole(4);
				switch (input)
				{
					case 1:
						StudentsFunctions.AddLecturesForStudent(dbContext);
						break;
					case 2:
						StudentsFunctions.ChangeDepartamentAndLecturesForStudent(dbContext);
						break;
					case 3:
						StudentsFunctions.DeleteStudent(dbContext);
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
