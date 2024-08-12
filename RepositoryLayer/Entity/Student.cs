using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        public int GradeId { get; set; }
        [ForeignKey("AddressId")]
        public virtual StudentAddress Address { get; set; }

        public int AddressId;
        [InverseProperty(nameof(StudentInCourse.Student))]
        public virtual ICollection<StudentInCourse> Courses { get; set; }
        public Student() { }

        public Student(string firstname, string lastname, string phone)
        {
            FirstName = firstname;
            LastName = lastname;
            Phone = phone;
        }

        public Student(string firstname, string lastname, string phone, StudentAddress studentAddress)
        {
            FirstName = firstname;
            LastName = lastname;
            Phone = phone;
            Address = studentAddress;
        }
    }
}
