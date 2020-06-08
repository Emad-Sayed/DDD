using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DistributorManagment.Commands.CreateDistributor;
using Application.DistributorManagment.Queries.ListDistributors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DistributorManagment
{

    [EnableCors("AllowOrigin")]
    [Route("api/" + nameof(Contexts.DistributorManagment) + "/[controller]")]
    public class DistributorsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ListDistributorsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDistributorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}