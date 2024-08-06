namespace BusinessServiceLayer
{
  public class StudentDTO
  {
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string FullName => FirstName + " " + LastName;
    public string GradeValue { get; set; }

   public StudentDTO() { }
    public StudentDTO(string firstname, string lastname, string phone)
    {
      this.FirstName = firstname;
      this.LastName = lastname;
      this.Phone = phone;
    }
  }
}
