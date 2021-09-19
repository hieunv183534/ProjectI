using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectI.WebApp.Core.Entities;
using ProjectI.WebApp.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Api.Controller
{
    public class EmployeesController : BaseController<Employee>
    {
        IEmployeeServices _employeeServices;
        public EmployeesController(IBaseServices<Employee> baseService, IEmployeeServices employeeServices) : base(baseService)
        {
            _employeeServices = employeeServices;
        }

        [HttpGet("filter")]
        public IActionResult GetFitler([FromQuery] int? positionId, [FromQuery] string searchTerms)
        {
            var serviceResult = _employeeServices.GetFilter(positionId, searchTerms);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }
    }
}
