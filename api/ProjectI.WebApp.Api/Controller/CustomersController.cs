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
    public class CustomersController : BaseController<Customer>
    {
        ICustomerServices _customerServices;
        public CustomersController(IBaseServices<Customer> baseService, ICustomerServices customerServices) : base(baseService)
        {
            _customerServices = customerServices;
        }

        [HttpGet("filter")]
        public IActionResult GetFitler( [FromQuery] int? customerTypeId, [FromQuery] string searchTerms)
        {
            var serviceResult = _customerServices.GetFilter(customerTypeId, searchTerms);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }
    }
}
