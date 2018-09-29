using AutoMapper;
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
    }
}