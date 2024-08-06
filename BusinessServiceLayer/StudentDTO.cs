namespace BusinessServiceLayer
{
  public class StudentDTO
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string FullName => FirstName + " " + LastName;
    public string GradeValue { get; set; }

    public StudentDTO(int id, string firstname, string lastname, string phone)
    {

      Id = id;
      FirstName = firstname;
      LastName = lastname;
      Phone = phone;
    }
  }
}
