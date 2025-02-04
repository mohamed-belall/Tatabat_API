using Talabat.Core.Entities;

namespace Talabat.API.Dtos
{
    public class EmployeeToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public int DepartmentId { get; set; }
        public String Department { get; set; }
    }
}
