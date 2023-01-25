using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentsIS.Entities
{
    public class Departament
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Lecture> Lectures { get; set; }

        public Departament(string name, List<Lecture> lectures) 
        { 
            Name = name;
            Lectures = lectures;
        }

        public Departament(int id, List<Lecture> lectures)
        {
            Id = id;
            Lectures = lectures;
        }

        public Departament(int id, string name, List<Lecture> lectures) 
        { 
            Id = id;
            Name = name;
            Lectures = lectures;
        }

        public Departament() { }
    }
}
