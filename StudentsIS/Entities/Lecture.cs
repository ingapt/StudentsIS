using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsIS.Entities
{
    public class Lecture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public string Teacher { get; set; }
        public List<Departament> Departaments { get; set; }

        public Lecture(string name, int credit, string teacher, List<Departament> departaments)
        { 
            Name = name;
            Credit = credit;
            Teacher = teacher;
            Departaments = departaments;     
        }

        public Lecture(string name, int credit, string teacher)
        {
            Name = name;
            Credit = credit;
            Teacher = teacher;

        }

        public Lecture(int id)
        { 
            Id = id;
        }

        public Lecture() { }
    }
}
