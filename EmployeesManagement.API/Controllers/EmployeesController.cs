using AutoMapper;
using EmployeesManagement.API.Helpers;
using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.DTO;
using EmployeesManagement.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            return Ok(_employeeService.UpsertEmployee(employeeDto));
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeDto, int id)
        {
            employeeDto.Id = id;
            _employeeService.UpsertEmployee(employeeDto);
            return Ok();
        }

        [HttpGet("{id:int}")]        
        public IActionResult Get(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound($"User with id {id} was not found");

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            employeeDto.Address = _mapper.Map<AddressDTO>(_addressService.GetAddressForEmployee(id));
            employeeDto.Salary = _mapper.Map<SalaryDTO>(_salaryService.GetSalaryForEmployee(id));
            return Ok(employeeDto);
        }

        [HttpGet("search")]
        public IActionResult GetSearchResults([FromQuery] SearchUrlQuery urlQuery)
        {
            var take = urlQuery.Take == 0 ? 30 : urlQuery.Take;

            urlQuery.OrderBy = urlQuery.OrderBy ?? "firstName;ASC";

            var searchResults= _employeeService.SearchEmployees(urlQuery.Q, urlQuery.Skip, take, urlQuery.OrderBy);

            return Ok(searchResults);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return Ok();
            }
            catch (EmployeeNotFoundException e)
            {
                return NotFound(e.Message);
            }
            
        }
    }
}