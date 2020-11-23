using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;
using Application.CustomerManagment.Commands.AddDeviceID;
using Application.CustomerManagment.Commands.CreateCustomer;
using Application.CustomerManagment.Commands.DeleteCustomer;
using Application.CustomerManagment.Commands.RemoveDeviceID;
using Application.CustomerManagment.Commands.UpdateCustomer;
using Application.CustomerManagment.Queries.CustomerById;
using Application.CustomerManagment.Queries.ListCities;
using Application.CustomerManagment.Queries.ListCustomers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerManagment
{
    [EnableCors()]
    [Route("api/" + nameof(Contexts.CustomerManagment) + "/[controller]")]
    public class CustomersController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(ListCustomersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById([FromQuery] CustomerByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            var user = User;
            var result = await Mediator.Send(command);
            return Ok( result );
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UpdateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("AddDeviceId")]
        [Authorize]
        public async Task<IActionResult> AddDeviceId([FromBody] AddDeviceIDCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("RemoveDeviceId")]
        [Authorize]
        public async Task<IActionResult> RemoveDeviceId([FromBody] RemoveDeviceIDCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("ActiveAndDeactiveCustomer")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> ActiveAndDeactiveCustomer([FromBody] ActiveAndDeactiveCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("Cities")]
        public async Task<IActionResult> GetAllCities([FromQuery] ListCitiesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}