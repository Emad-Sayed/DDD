using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.DeleteProductCategory;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.UpdateProductCategory;
using Application.ProductCatalog.ProductCategoryAggregate.Queries.ListProductCategories;
using Application.ProductCatalog.ProductCategoryAggregate.Queries.ProductCategoryList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers.ProductCatalog
{
    [EnableCors()]
    [Authorize]
    [Route("api/" + nameof(Contexts.ProductCatalog) + "/[controller]")]
    public class ProductCategoriesController : BaseController
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(ListAllProductCategoriesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(ListProductCategoriesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Post([FromBody] CreateProductCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Put([FromBody] UpdateProductCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpDelete]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Delete([FromQuery] DeleteProductCategoryCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}