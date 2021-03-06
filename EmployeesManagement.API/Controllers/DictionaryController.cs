﻿using EmployeesManagement.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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