using StudentsIS.Entities;

namespace StudentsIS.Classes
{
    public static class Dispay
    {
        public static void DisplayDepartaments(this StudentContext dbContext)
        {
            var departaments = dbContext.Departaments.ToList();
            
            foreach (var departament in departaments)
            {
                Console.WriteLine($"{departament.Name}");
            }
        }
    }
}
