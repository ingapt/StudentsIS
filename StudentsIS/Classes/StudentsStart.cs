
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
				Console.WriteLine("[1] Sukurti studentą \n[2] Įterpti studentą į departamentą \n[3] Priskirti studentui paskaitas \n[4] Perkelti studentą į kitą departamentą ir pakeisti jo paskaitas  \n[5] Ištrinti paskaitą \n[6] Grįžti atgal");
				var input = Validation.GetValidNumbersFromConsole(4);
				switch (input)
				{
					case 1:

						break;
					case 2:
						DepartamentsFunctions.InsertExistingStudentToDepartament(dbContext);
						break;
					case 3:

						break;
					case 4:
						break;
					case 5:
						LecturesFunctions.DeleteLecture(dbContext);
						break;
					case 6:
						toDo = false;
						break;
					default:
						break;
				}
			}
		}

	}
}
