using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class EmployeeWithDepartmentSpecifications : BaseSpecifications<Employee>
    {

        public EmployeeWithDepartmentSpecifications() :base()
        {
            //Criteria = null;
            Includes.Add(p => p.Department);
        }
    }
}
