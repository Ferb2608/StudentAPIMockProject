using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessServiceLayer.DTO
{
    public class StudentInCourseDTO
    {
        [JsonIgnore]
        public int Id {  get; set; }
        public CourseDTO Course { get; set; }
        [JsonIgnore]
        public StudentDTO Student { get; set; }
    }
    public class StudentInCourseInputDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
