using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class StudentAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public virtual Student Student { get; set; }
        public StudentAddress() { }
        public StudentAddress(string street, string city, string province, string country, string postalCode) {
            this.Street = street;
            this.City = city;
            this.Province = province;
            this.Country = country;
            this.PostalCode = postalCode;
        }
    }
}
