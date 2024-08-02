using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GradeValue { get; set; }
        //public Student Student { get; set; } = new Student();
        public Grade()
        {

        }
        public Grade(string gradeValue)
        {
            GradeValue = gradeValue;
        }
    }
}
