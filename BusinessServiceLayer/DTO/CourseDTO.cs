using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessServiceLayer.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public CourseDTO() { }
    }
    public class InputCourseDTO
    {
        public string CourseName { get; set; }
    }
}
