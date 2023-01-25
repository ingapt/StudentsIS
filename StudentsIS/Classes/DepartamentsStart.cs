namespace StudentsIS.Classes
{
    public static class DepartamentsStart
    {
        public static void Start(this StudentContext dbContext)
        {
            bool toDo = true;

            while (toDo)
            {
                Console.Clear();
                Console.WriteLine("Pasirinkite: ");
                Console.WriteLine("[1] Sukurti Departamentą \n[2] Įterpti paskaitas/studentus į departamentą \n[3] Grįžti atgal");
                var input = Validation.GetValidNumbersFromConsole(3);
                switch (input)
                {
                    case 1:
                        DepartamentsFunctions.CreateDepartament(dbContext);
                        break;
                    case 2:
                        DepartamentsInsertions.Start(dbContext);
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
