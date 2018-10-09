using System;
using Dapper;
using EmployeesManagement.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService service)
        {
            _statsService = service;
        }


        [Route("avgSalary")]
        [HttpGet]
        public IActionResult GetMonthsAverageSalary()
        {
            var response = new
            {
                timeStamp = DateTime.Now,
                data = _statsService.GetAverageMonthsSalary().AsList()
            };

            return Ok(response);
        }

        [Route("employees")]
        [HttpGet]
        public IActionResult GetEmployeesStats()
        {
            return Ok(_statsService.GetEmployeesStats());
        }
    }
}