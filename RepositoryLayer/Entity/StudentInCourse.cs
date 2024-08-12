using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Entity
{
    [Index(nameof(StudentId), nameof(CourseId), IsUnique = true)]
    public class StudentInCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("Courses")]
        public virtual Student Student { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [InverseProperty("Students")]
        public virtual Course Course { get; set; }

    }
}
