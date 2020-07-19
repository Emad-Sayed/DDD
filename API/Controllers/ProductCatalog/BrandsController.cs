using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.BrandAggregate.Commands.DeleteBrand;
using Application.ProductCatalog.BrandAggregate.Commands.UpdateBrand;
using Application.ProductCatalog.BrandAggregate.Queries.BrandList;
using Application.ProductCatalog.BrandAggregate.Queries.ListBrands;
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
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(ListAllBrandsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(ListBrandsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Post([FromBody]CreateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Put([FromBody] UpdateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpDelete]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Delete([FromQuery] DeleteBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}