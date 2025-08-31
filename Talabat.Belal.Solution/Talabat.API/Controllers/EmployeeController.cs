using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.API.Controllers
{

    public class EmployeeController : BaseApiController
    {
        private readonly IGenericRepository<Employee> _empRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IGenericRepository<Employee> EmpRepo  ,IMapper mapper)
        {
            _empRepo = EmpRepo;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployee()
        //{

        //    var spec = new EmployeeWithDepartmentSpecifications();

        //    var emps = await _empRepo.GetAllWithSpecAsync(spec);

        //    if (emps is null || emps.Count() == 0)
        //        return NotFound("there is no employees");

        //    return Ok(emps);
        //}


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeToReturnDTO>>> GetAllEmployee()
        {

            var spec = new EmployeeWithDepartmentSpecifications();

            var emps = await _empRepo.GetAllWithSpecAsync(spec);

            //var newEmps = new Employee();

            //_empRepo.AddAsync(newEmps);


            if (emps is null || emps.Count() == 0)
                return NotFound("there is no employees");

            return Ok(_mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeToReturnDTO>>(emps));
        }

    }
}
