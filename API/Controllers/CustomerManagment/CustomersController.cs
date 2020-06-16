using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;
using Application.CustomerManagment.Commands.CreateCustomer;
using Application.CustomerManagment.Commands.DeleteCustomer;
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
        public async Task<IActionResult> GetById([FromQuery]CustomerByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Delete([FromQuery]DeleteCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("Cities")]
        public async Task<IActionResult> GetAllCities([FromQuery]ListCitiesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}