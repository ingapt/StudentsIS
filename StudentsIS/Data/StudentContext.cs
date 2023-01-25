using Microsoft.EntityFrameworkCore;
using StudentsIS.Entities;

namespace StudentsIS
{
    public class StudentContext : DbContext
    {
        public DbSet<Departament> Departaments { get; set; }   
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=192.168.10.224; Database=StudentsDb; Uid=inga; Pwd=pomidoras; Trusted_connection=False; TrustServerCertificate=True; MultipleActiveResultSets=True;");
        }
    }
}
