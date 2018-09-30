using AutoMapper;
using EmployeesManagement.Core.DTO;
using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();

            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            CreateMap<Salary, SalaryDTO>();
            CreateMap<SalaryDTO, Salary>();
        }
    }
}
