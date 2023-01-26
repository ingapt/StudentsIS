using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsIS.Entities
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Lecture> Lectures { get; set; } = new List<Lecture>();

        [ForeignKey("Departament")]
        public int? DepartamentId { get; set; }
        public Departament Departament { get; set; }

        public Student(string name, string surname, List<Lecture> lectures, int departamentId)
        {
            Name = name;
            Surname = surname;
            Lectures = lectures;
            DepartamentId = departamentId;
        }

        public Student (string name, string surname, int departamentId)
        {
            Name = name;
            Surname = surname;
            DepartamentId = DepartamentId;
        }

		public Student(string name, string surname)
		{
			Name = name;
			Surname = surname;
		}


		public Student(int id)
        {
            Id = id;
        }

        public Student() { }
    }
}
