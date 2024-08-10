using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClassName { get; set; }
        [ForeignKey("Grade")]
        public virtual Grade Grade { get; set; }

        [ForeignKey("Student")]
        public virtual List<Student> Student { get; set; }


        public Class() { }

        public Class(Grade grade, string className)
        {
            Grade = grade;
            ClassName = className;
        }

        public Class(Grade grade, string className, List<Student> students)
        {
            Grade = grade;
            ClassName = className;
            Student = students;
        }

    }
}
