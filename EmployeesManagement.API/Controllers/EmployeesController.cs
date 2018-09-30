using AutoMapper;
using EmployeesManagement.API.Helpers;
using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAddressService _addressService;
        private readonly ISalaryService _salaryService;
        private readonly IMapper _mapper;


        public EmployeesController(
            IEmployeeService employeeService, 
            IAddressService addressService,
            ISalaryService salaryService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _addressService = addressService;
            _salaryService = salaryService;
            _mapper = mapper;

        }

        [HttpGet("{id:int}")]        
        public IActionResult Get(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound($"User with id {id} was not found");

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            var address = _addressService.GetAddressForEmployee(id);
            employeeDTO.Address = _mapper.Map<AddressDTO>(address);
            employeeDTO.Salary = _mapper.Map<SalaryDTO>(_salaryService.GetSalaryForEmployee(id));
            return Ok(employeeDTO);
        }

        [HttpGet("search")]
        public IActionResult GetSearchResults([FromQuery] SearchUrlQuery urlQuery)
        {
            int take = urlQuery.Take == 0 ? 30 : urlQuery.Take;
            

            var sql = @"
                    SELECT Employee.id, Employee.firstName, Employee.lastName, Employee.email, S.amount as CurrentSalary, GROUP_CONCAT(PositionName) as PositionsNames FROM Employee
                    join Salary S on Employee.id = S.employee_id
                    left join PositionEmployee E on Employee.id = E.EmployeeID
                    left join Position P on E.PositionId = P.Id
                    where (current_date() between S.fromDate and S.toDate)
                    and MATCH(Employee.firstName, Employee.lastName, Employee.email)
                    AGAINST('@SearchTerm' IN BOOLEAN MODE)
                    group by Employee.id, S.amount
                    order by Employee.firstName
                    limit @skip @take";




            return Ok("XD");
        }
    }
}