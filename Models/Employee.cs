using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Company PlaceOfWork { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string DisplayName()
        {
            return "Name: " + FirstName + " " + LastName;
        }

        public string DisplayCompany()
        {
            return "Company: " + PlaceOfWork.Name;
        }

        public string DisplayNumber()
        {
            return "Number: " + PhoneNumber;
        }

        public string DisplayEmail()
        {
            return "Email: " + Email;
        }
    }
}
