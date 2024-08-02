using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer
{
  public class Student
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public Grade Grade { get; set; }

    public Student() { }

    public Student(string firstname, string lastname, string phone)
    {
      FirstName = firstname;
      LastName = lastname;
      Phone = phone;
    }
  }
}
