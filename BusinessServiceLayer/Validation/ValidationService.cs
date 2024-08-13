using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Validation
{
    public class ValidationService
    {
        public ValidationService() { }
        public bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return false;
            string pattern = @"^([\+]?84[-]?|[0])?(9|8|7|5|3)[0-9]{8}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }
        public bool ValidateGradeValue(string gradeValue)
        {
            if (string.IsNullOrEmpty(gradeValue))
                return false;
            string pattern = @"^\d{1,2}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(gradeValue))
            {
                int number = int.Parse(gradeValue);
                return number < 12;
            }
            return false;
        }
    }
}
