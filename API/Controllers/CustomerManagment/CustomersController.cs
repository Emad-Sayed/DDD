using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CustomerManagment.Commands.CreateCustomer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerManagment
{
    [EnableCors("AllowOrigin")]
    [Route("api/" + nameof(Contexts.CustomerManagment) + "/[controller]")]
    public class CustomersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}