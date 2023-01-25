namespace StudentsIS.Classes
{
    public static class DepartamentsInsertions
    {
        public static void Start(this StudentContext dbContext)
        {
            bool toDo = true;

            while (toDo)
            { 
                Console.Clear();
                Console.WriteLine("Pasirinkite");
                Console.WriteLine("[1] Įterpti studentą į departamentą \n[2] Įterpti paskaitą į departamentą \n[3] Grįžti atgal");
                var input = Validation.GetValidNumbersFromConsole(3);
                switch (input)
                {
                    case 1:
                        DepartamentsFunctions.InsertStudentToDepartament(dbContext);
                        break;
                    case 2:
                        DepartamentsFunctions.InsertLectureToDepartament(dbContext);
                        break;
                    case 3:
                        toDo= false;
                        break;
                    default:
                        break;
                }   
            }

        }
    }
}
