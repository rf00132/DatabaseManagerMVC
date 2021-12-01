using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class CompanyLogo
    {

        public int Id { get; set; }
        public object Logo { get; set; }
        public Company CompanyAssociated { get; set; }
    }
}
