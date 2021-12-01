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

        public string DisplayText()
        {
            string retstring = string.Format("Name: {0} {1}", FirstName, LastName);
  
            //if (Email != "") retstring += ", Email: " + Email;
           
            if (PlaceOfWork != null) retstring += ", Place of Work: " + PlaceOfWork.Name;
            
            //if (PhoneNumber != "") retstring += ", Phone Number: " + PhoneNumber;
            
            return retstring;
        }
    }
}
