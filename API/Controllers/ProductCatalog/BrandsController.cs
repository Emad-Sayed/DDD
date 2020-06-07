using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.BrandAggregate.Queries.BrandList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductCatalog
{
    [EnableCors()]
    [Authorize]
    [Route("api/" + nameof(Contexts.ProductCatalog) + "/[controller]")]
    public class BrandsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(BrandListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Post([FromBody]CreateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}