using Azure.Identity;

namespace StudentsIS.Classes
{
    public static class Init
    {
        public static void Start()
        {
            using var dbContext = new StudentContext();

            bool toDo = true;

            while (toDo)
            {
                Console.Clear();
                Console.WriteLine("Pasirinkite: ");
                Console.WriteLine("[1] Departamentai \n[2] Paskaitos \n[3] Studentai \n[4] Atvaizdavimas \n[5] Baigti");
                var input = Validation.GetValidNumbersFromConsole(5);
                switch (input)
                {
                    case 1:
                        DepartamentsStart.Start(dbContext);
                        break;
                    case 2:
                        LecturesStart.Start(dbContext);
                        break;
                    case 3:
                        StudentsStart.Start();
                        break;
                    case 4:
                        DisplayStart.Start();
                        break;
                    case 5:
                        toDo= false;
                        break;
                    default:
                        break;

                }
            }
        }
    }
}
