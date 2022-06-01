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
    
        public string DisplayName()
        {
            return "Name: " + Name;
        }

        public string DisplayWebsite()
        {
            return "Website: " + Website;
        }

        public string DisplayEmail()
        {
            return "Email: " + Email;
        }
    }
}
