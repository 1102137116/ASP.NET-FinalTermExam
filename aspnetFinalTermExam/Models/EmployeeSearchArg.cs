using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnetFinalTermExam.Models
{
    public class EmployeeSearchArg
    {
        public string EmployeeID { get; set; }
        public string EmpName{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CodeId { get; set; }
        public string HireDate { get; set; }
    }
}