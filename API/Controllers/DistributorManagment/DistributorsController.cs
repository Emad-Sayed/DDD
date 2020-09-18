using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;
using Application.DistributorManagment.Commands.ConfirmDistributorUserEmail;
using Application.DistributorManagment.Commands.CreateCity;
using Application.DistributorManagment.Commands.CreateDistributor;
using Application.DistributorManagment.Commands.CreateDistributorUser;
using Application.DistributorManagment.Commands.DeleteCity;
using Application.DistributorManagment.Commands.DeleteDistributor;
using Application.DistributorManagment.Commands.DeleteDistributorUser;
using Application.DistributorManagment.Commands.RemoveDistributorAreas;
using Application.DistributorManagment.Commands.UpdateCity;
using Application.DistributorManagment.Commands.UpdateDistributor;
using Application.DistributorManagment.Commands.UpdateDistributorUser;
using Application.DistributorManagment.Queries.DistributorById;
using Application.DistributorManagment.Queries.ListCities;
using Application.DistributorManagment.Queries.ListDistributors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DistributorManagment
{

    [EnableCors()]
    [Route("api/" + nameof(Contexts.DistributorManagment) + "/[controller]")]
    public class DistributorsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ListDistributorsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{distributorId}")]
        public async Task<IActionResult> GetById([FromQuery] DistributorByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDistributorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateDistributorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpDelete]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Delete([FromQuery] DeleteDistributorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("RemoveDistributorAreas")]
        public async Task<IActionResult> RemoveDistributorAreas([FromBody] RemoveDistributorAreasCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        #region Cities and Areas
        [HttpGet("Cities")]
        public async Task<IActionResult> GetAllCities([FromQuery] ListCitiesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Cities")]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Cities")]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateCityCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Cities/{cityId}")]
        public async Task<IActionResult> DeleteCity([FromRoute] DeleteCityCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        #endregion


        [HttpPost("{distributorId}/CreateDistributorUser")]
        public async Task<IActionResult> CreateDistributorUserCommand([FromBody] CreateDistributorUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPut("{distributorId}/UpdateDistributorUser")]
        public async Task<IActionResult> UpdateDistributorUser([FromBody] UpdateDistributorUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }


        [HttpPost("ConfirmDistributorUserEmail")]
        public async Task<IActionResult> Get([FromBody] ConfirmDistributorUserEmailCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{distributorId}/DeleteDistributorUser")]
        public async Task<IActionResult> DeleteDistributorUser([FromQuery] DeleteDistributorUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}