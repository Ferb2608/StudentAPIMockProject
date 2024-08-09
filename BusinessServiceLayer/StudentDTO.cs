using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public AddressDTO AddressDTO { get; set; }
        public string Address 
        { get => AddressDTO.Street + ", " + AddressDTO.City + ", " + AddressDTO.Province + ", " + AddressDTO.Country + ", " + AddressDTO.PostalCode; set { } }

        public StudentDTO() { }
        public StudentDTO(string firstname, string lastname, string phone)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Phone = phone;
        }
    }

    public class AddressDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
    public class StudentInputDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string GradeValue { get; set; }
        public AddressDTO Address { get; set;   }
    }
}
