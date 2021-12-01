using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Company
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        //public bool HasLogo { get; set; }
    
        public string DisplayText()
        {
            string retstring = "Name: " + Name;
            if(Website != "") retstring += ", Website: " + Website;
            if(Email != "") retstring += ", Email: " + Email;

            return retstring;
        }
    
    }
}
