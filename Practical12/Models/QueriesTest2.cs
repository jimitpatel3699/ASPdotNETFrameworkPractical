using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practical12.Models
{

    public class QueriesTest2
    {
        public IEnumerable<EmployeeTesttwo> EmployeesWithMiddleNameAsNull { get; set; }
        public IEnumerable<EmployeeTesttwo> EmployeesWithDOBLessThan { get; set; }
        public decimal TotalSalary { get; set; }
    }

}