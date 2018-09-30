using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesManagement.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService service)
        {
            _dictionaryService = service;
        }

        [Route("positions")]
        [HttpGet]
        public IActionResult GetPositionsDictionary()
        {
            return Ok(_dictionaryService.GetPositionsDictionary());
        }
    }
}