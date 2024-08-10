using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GradeValue { get; set; }
        [NotMapped]
        public virtual List<Student> Students { get; set; }
        public Grade()
        {

        }
        public Grade(string gradeValue)
        {
            GradeValue = gradeValue;
        }
    }
}
